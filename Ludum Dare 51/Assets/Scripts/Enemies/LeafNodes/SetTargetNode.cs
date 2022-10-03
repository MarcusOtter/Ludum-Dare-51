using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetNode : Node
{
    private Transform customTarget;
    public SetTargetNode(Transform target, EnemyAgent agent) : base(agent)
    {
        customTarget = target;
    }
    public override NodeStatus Run()
    {
        agent.target = customTarget;
        return agent.target == null ? NodeStatus.FAILURE : NodeStatus.SUCCESS;
    }
}
