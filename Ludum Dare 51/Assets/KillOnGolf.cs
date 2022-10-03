using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnGolf : MonoBehaviour
{
    private void OnEnable()
    {
        GolfGoal.OnGolfStart += KillMe;
    }

    private void OnDisable()
    {
        GolfGoal.OnGolfStart -= KillMe;
    }
    void KillMe()
    {
        Destroy(gameObject);
    }
}
