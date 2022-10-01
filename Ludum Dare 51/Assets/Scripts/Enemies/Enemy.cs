using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static Action OnDie;

    public int health;
    public bool TakeDamage(int damage)
    {
        health -= damage;
        return health > 0;
    }

    public void Die()
    {
        OnDie.Invoke();
        //Do death stuff
    }
}
