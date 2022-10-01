using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KillEnemies", menuName = "ScriptableObjects/GameGoals/KillEnemies", order = 1)]
public class KillEnemiesGoal : GameGoal
{
    public int PointsPerKill = 1;
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Do some stuff on enter!");
    }
    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("Do some stuff on exit!");
    }
}
