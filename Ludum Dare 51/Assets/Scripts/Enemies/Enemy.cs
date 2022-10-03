using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static Action OnDie;

    public int health;
    public float movementSpeed;
    [SerializeField] private float range = 10f;

    protected PlayerMovement player;
    protected virtual void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
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

    public void Die()
    {
        OnDie?.Invoke();
        //Do death stuff
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            //kill player
        }
    }
}
