using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachTarget : Node
{
    public ApproachTarget(EnemyAgent agent) : base(agent)
    {}
    public override NodeStatus Run()
    {
        Vector3 direction = (agent.target.position - agent.transform.position);
        agent.transform.forward = Vector3.Lerp(agent.transform.forward, direction.normalized, 0.6f);

        agent.rigidbody.AddForce(agent.transform.forward * agent.movementSpeed);
        return NodeStatus.RUNNING;
    }
}
