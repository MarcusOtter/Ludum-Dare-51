using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum NodeStatus
{
    SUCCESS,
    FAILURE,
    RUNNING
};
[System.Serializable]
public abstract class Node
{
    protected string debugString;
    protected EnemyAgent agent;
    protected NodeStatus _status;
    protected Node(EnemyAgent agent = null)
    {
        this.agent = agent;
        _status = NodeStatus.FAILURE;
    }

    public abstract NodeStatus Run();
    public virtual void DebugPrint()
    {
        Debug.Log(debugString + " " + GetCurrentStatus().ToString());
    }
    public NodeStatus GetCurrentStatus()
    {
        return _status;
    }
}

#region COMPOSITES
[System.Serializable]
public abstract class Composite : Node
{
    public List<Node> children = new List<Node>();
    public void AddChild(Node child)
    {
        children.Add(child);
    }
}
[System.Serializable]
public class Sequence : Composite
{
    public Sequence() : base()
    {
    }

    public override NodeStatus Run()
    {
        bool hasRunningChild = false;
        foreach(var c in children)
        {
            var status = c.Run();
            switch(status)
            {
                case NodeStatus.RUNNING:
                    hasRunningChild = true;
                    return NodeStatus.RUNNING;
                
                case NodeStatus.FAILURE:
                    return NodeStatus.FAILURE;                   

                default:
                break;
            }
        }
        _status =  hasRunningChild ? NodeStatus.RUNNING : NodeStatus.SUCCESS;
        return _status;
    }
}
[System.Serializable]
public class Selector : Composite
{
    public override NodeStatus Run()
    {
        foreach (var c in children)
        {
            var status = c.Run();
            if (status == NodeStatus.SUCCESS || status == NodeStatus.RUNNING)
            {
                _status = status;
                return status;
            }
        }
        _status = NodeStatus.FAILURE;
        return _status;
    }
}
#endregion
#region DECORATORS
[System.Serializable]
public abstract class Decorator : Node
{
    public Node Child;
    public Decorator(Node child)
    {
        Child = child;
    }
}
[System.Serializable]
public class Inverter : Decorator
{
    public Inverter(Node child) : base(child)
    {

    }
    public override NodeStatus Run()
    {
        switch(Child.Run())
        {
            case NodeStatus.SUCCESS:
                return NodeStatus.FAILURE;

            case NodeStatus.FAILURE:
                return NodeStatus.SUCCESS;

            default:
                return NodeStatus.RUNNING;
        }
    }
}
[System.Serializable]
public class FailureToRunning : Decorator
{
    public FailureToRunning(Node child) : base(child)
    {

    }
    public override NodeStatus Run()
    {
        switch (Child.Run())
        {
            case NodeStatus.SUCCESS:
                return NodeStatus.SUCCESS;
            case NodeStatus.FAILURE:
                return NodeStatus.RUNNING;
            case NodeStatus.RUNNING:
                return NodeStatus.RUNNING;
            default:
                Debug.LogError($"{Child.GetType().Name} returns a nonexistent nodestatus. I don't know how that's even possible");
                return NodeStatus.SUCCESS;
        }
    }
}
#endregion
