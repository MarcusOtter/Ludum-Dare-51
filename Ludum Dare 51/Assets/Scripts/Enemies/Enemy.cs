using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IHurtable
{
    public static Action OnDie;

    public int health;
    public float movementSpeed;
    [SerializeField] private float range = 10f;
    protected bool IsDead;

    protected PlayerMovement player;
    protected virtual void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    public bool IsLethalDamage(int damage)
    {
        return health - damage <= 0;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    public float Range()
    {
        return range;
    }

    public virtual void Die()
    {
        OnDie?.Invoke();
        IsDead = true;
        
        // TODO: Spawn corpse
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            //kill player
        }
    }
}
