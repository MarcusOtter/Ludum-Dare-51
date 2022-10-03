using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SetTargetToClosest : Node
{
    private List<Transform> _targetsToChooseFrom;
    public SetTargetToClosest(List<Transform> l, EnemyAgent agent) : base(agent)
    {
        _targetsToChooseFrom = l;
    }
    public override NodeStatus Run()
    {
        if (_targetsToChooseFrom.Count == 0) return NodeStatus.FAILURE;
        agent.target = _targetsToChooseFrom.OrderBy(x => (x.transform.position - agent.transform.position).sqrMagnitude).First();
        return NodeStatus.SUCCESS;
    }
}
