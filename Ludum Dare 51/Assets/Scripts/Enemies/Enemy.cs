using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static Action OnDie;

    public int health;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    public void Die()
    {
        OnDie?.Invoke();
        Destroy(gameObject);
        //Do death stuff
    }
}
