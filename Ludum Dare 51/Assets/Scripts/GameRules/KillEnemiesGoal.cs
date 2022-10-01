using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KillEnemies", menuName = "ScriptableObjects/GameGoals/KillEnemies", order = 1)]
public class KillEnemiesGoal : GameGoal
{
    public int PointsPerKill = 1;

    public void GetPointsForKill()
    {
        GameManager.AddPoints(PointsPerKill);
        if (GameManager.PrintALot) Debug.Log($"+{PointsPerKill} points for a kill!");
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Enemy.OnDie += GetPointsForKill;
    }
    public override void OnExit()
    {
        base.OnExit();
        Enemy.OnDie -= GetPointsForKill;
    }
}
