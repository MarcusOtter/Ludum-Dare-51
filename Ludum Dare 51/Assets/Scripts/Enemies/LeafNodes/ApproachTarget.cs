using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachTarget : Node
{
    public ApproachTarget(EnemyAgent agent) : base(agent)
    {}
    public override NodeStatus Run()
    {
        Vector3 direction = (agent.target.position.With(y: agent.transform.position.y) - agent.transform.position);
        agent.transform.forward = Vector3.Lerp(agent.transform.forward, direction.normalized, 10f * Time.deltaTime);
        agent.rigidbody.AddForce(agent.transform.forward * agent.movementSpeed);
        return NodeStatus.RUNNING;
    }
}
