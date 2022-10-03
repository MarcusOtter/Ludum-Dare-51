using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyAgent
{
    private List<Transform> _nearbyWeapons = new();
    private Transform _customTarget;
    public Weapon HeldWeapon;

    [SerializeField] private float minNewTargetWait = 1, maxNewTargetWait = 5;
    [SerializeField] private float minShootWait = 1, maxShootWait = 5;
    [SerializeField] private float autoShootMin = 0.2f, autoShootMax = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        _customTarget = new GameObject("TargetPosition").transform;
        StartCoroutine(WanderAround());
        StartCoroutine(ShootRandomly());
    }
    private void OnDestroy()
    {
        if (_customTarget != null && _customTarget.gameObject != null)
        {
            Destroy(_customTarget.gameObject);
        }
    }
    protected override void InitializeBehaviourTree()
    {
        Selector baseSelector = new();

        //{
        //    Sequence approachPlayerSequence = new();

        //    SetTargetNode setTargetNode = new(player.transform, this);
        //    WithinRange withinRange = new(this);
        //    ApproachTarget approach = new(this);

        //    approachPlayerSequence.children.Add(setTargetNode);
        //    approachPlayerSequence.children.Add(withinRange);
        //    approachPlayerSequence.children.Add(approach);

        //    baseSelector.children.Add(approachPlayerSequence);
        //}

        {
            Sequence approachWeapon = new();

            Unequiped isUnequiped = new(this);
            IsListEmpty<Transform> noNearbyWeapons = new(_nearbyWeapons); Inverter areNearbyWeapons = new(noNearbyWeapons);
            SetTargetToClosest chooseClosest = new(_nearbyWeapons, this);
            ApproachTarget approach = new(this);

            approachWeapon.AddChild(isUnequiped);
            approachWeapon.AddChild(areNearbyWeapons);
            approachWeapon.AddChild(chooseClosest);
            approachWeapon.AddChild(approach);

            baseSelector.AddChild(approachWeapon);
        }
        {
            Sequence wanderAimlessly = new();

            SetTargetNode setCustomTarget = new(_customTarget, this);
            ApproachTarget approach = new(this);

            wanderAimlessly.AddChild(setCustomTarget);
            wanderAimlessly.AddChild(approach);

            baseSelector.AddChild(wanderAimlessly);
        }

        root = baseSelector;
    }

    void ChooseRandomTargetPosition()
    {
        _customTarget.transform.position = Random.insideUnitCircle * 9f;
        _customTarget.position = _customTarget.position.With(z: _customTarget.position.y);
    }


    private IEnumerator WanderAround()
    {
        while (true)
        {
            ChooseRandomTargetPosition();
            yield return new WaitForSeconds(Random.Range(minNewTargetWait, maxNewTargetWait));
        }
    }

    private IEnumerator ShootRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minShootWait, maxShootWait));
            if (HeldWeapon && HeldWeapon.isAutomatic)
            {
                float starttime = Time.time;
                float duration = Random.Range(autoShootMin, autoShootMax);
                while (Time.time < starttime + duration)
                {
                    HeldWeapon?.Attack();
                    yield return null;
                }
            }
            else
            {
                HeldWeapon?.Attack();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print($"Collide with {other.name}!");
        if (other.GetComponentInParent<Weapon>())
        {
            _nearbyWeapons.Add(other.transform);
            print("Add weapon!");
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Weapon>())
        {
            _nearbyWeapons.Remove(other.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        var weapon = collision.gameObject.GetComponentInParent<Weapon>();
        if (weapon)
        {
            if (HeldWeapon) HeldWeapon.Drop(transform.position - transform.forward);
            HeldWeapon = weapon;
            weapon.PickUp(transform);
            weapon.transform.localPosition = Vector3.zero;
        }
    }
}
