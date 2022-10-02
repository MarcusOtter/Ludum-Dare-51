using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Swap Weapon", menuName = "ScriptableObjects/GameGoals/SwapWeapon", order = 1)]
public class SwapWeapon : GameGoal
{
    public int PointsPerSwap = 1;

    public void GetPointsForWeaponSwap()
    {
        GameManager.AddPoints(PointsPerSwap);
        if (GameManager.PrintALot) Debug.Log($"+{PointsPerSwap} points for a kill!");
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Enemy.OnDie += GetPointsForWeaponSwap;
    }
    public override void OnExit()
    {
        base.OnExit();
        Enemy.OnDie -= GetPointsForWeaponSwap;
    }
}
