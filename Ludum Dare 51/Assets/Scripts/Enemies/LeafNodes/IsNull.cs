using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsNull<T> : Node
{
    private T _thingToCheck;
    public IsNull(T l)
    {
        _thingToCheck = l;
    }
    public override NodeStatus Run()
    { 
        return _thingToCheck == null ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
    }
}
