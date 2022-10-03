using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsListEmpty<T> : Node
{
    private List<T> _listToCheck;
    public IsListEmpty(List<T> l)
    {
        _listToCheck = l;
    }
    public override NodeStatus Run()
    {
        return _listToCheck.Count == 0 ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
    }
}
