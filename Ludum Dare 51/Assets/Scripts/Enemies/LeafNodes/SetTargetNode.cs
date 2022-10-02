using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetNode : Node
{
    public SetTargetNode(Transform target, EnemyAgent agent) : base(agent)
    {
        agent.target = target;
    }
    public override NodeStatus Run()
    {
        return agent.target == null ? NodeStatus.FAILURE : NodeStatus.SUCCESS;
    }
}
