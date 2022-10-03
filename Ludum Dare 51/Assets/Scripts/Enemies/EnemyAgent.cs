using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class EnemyAgent : Enemy
{
    public Node root = null;
    public bool running = true;
    public Transform target;
    public Rigidbody rigidbody;

    private TMP_Text text;

    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        InitializeBehaviourTree();
        text = GetComponentInChildren<TMP_Text>();
    }
    protected abstract void InitializeBehaviourTree();

    public void SetText(string message)
    {
        text.text = message;
    }

    protected virtual void FixedUpdate()
    {
        if (running)
        {
            root?.Run();
        }
    }
}
