using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithinRange : Node
{
    public WithinRange(EnemyAgent agent) : base(agent)
    {
    }

    public override NodeStatus Run()
    {
        return (agent.target.position - agent.transform.position).sqrMagnitude < agent.Range() * agent.Range() ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
    }
}
