using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : EnemyAgent
{
    private List<Transform> _nearbyCorpses = new();

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void InitializeBehaviourTree()
    {
        Selector baseSelector = new();

        {
            Sequence approachWeapon = new();

            IsListEmpty<Transform> noNearbyCorpses = new(_nearbyCorpses); Inverter areNearbyCorpses = new(noNearbyCorpses);
            SetTargetToClosest chooseClosest = new(_nearbyCorpses, this);
            ApproachTarget approach = new(this);

            approachWeapon.AddChild(areNearbyCorpses);
            approachWeapon.AddChild(chooseClosest);
            approachWeapon.AddChild(approach);

            baseSelector.AddChild(approachWeapon);
        }

        {
            Sequence approachPlayerSequence = new();

            SetTargetNode setTargetNode = new(player.transform, this);
            WithinRange withinRange = new(this);
            ApproachTarget approach = new(this);

            approachPlayerSequence.children.Add(setTargetNode);
            approachPlayerSequence.children.Add(withinRange);
            approachPlayerSequence.children.Add(approach);

            baseSelector.children.Add(approachPlayerSequence);
        }

        root = baseSelector;
    }

    private void OnTriggerEnter(Collider other)
    {
        print($"Collide with {other.name}!");
        if (other.CompareTag("Corpse"))
        {
            _nearbyCorpses.Add(other.transform);
            print("Add corpse!");
        }

    }
    private void OnTriggerExit(Collider other)
    {
        _nearbyCorpses.Remove(other.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Corpse"))
        {
            StartCoroutine(EatCorpse());
            _nearbyCorpses.Remove(collision.transform);
            Destroy(collision.gameObject, 3f);
        }
    }
    private IEnumerator EatCorpse()
    {
        running = false;
        yield return new WaitForSeconds(3f);
        running = true;
        yield return null;
    }
}

