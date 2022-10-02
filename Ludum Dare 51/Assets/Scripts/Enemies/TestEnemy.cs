using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyAgent
{
    protected override void InitializeBehaviourTree()
    {
        Sequence approachPlayerSequence = new();
        SetTargetNode setTargetNode = new(player.transform, this);
        WithinRange withinRange = new(this);
        ApproachTarget approach = new(this);
        approachPlayerSequence.children.Add(setTargetNode);
        approachPlayerSequence.children.Add(withinRange);
        approachPlayerSequence.children.Add(approach);
        root = approachPlayerSequence;
    }
}
