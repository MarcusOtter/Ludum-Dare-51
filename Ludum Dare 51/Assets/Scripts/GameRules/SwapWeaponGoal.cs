using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwapWeapon", menuName = "ScriptableObjects/GameGoals/SwapWeapon", order = 1)]
public class SwapWeaponGoal : GameGoal
{
    public int PointsPerSwap = 1;

    public void GetPointsForWeaponSwap(Weapon _)
    {
        GameManager.AddPoints(PointsPerSwap);
        if (GameManager.PrintALot) Debug.Log($"+{PointsPerSwap} points for a kill!");
    }

    public override void OnEnter()
    {
        PlayerWeapon.OnWeaponPickedUp += GetPointsForWeaponSwap;
    }
    public override void OnExit()
    {
        PlayerWeapon.OnWeaponPickedUp -= GetPointsForWeaponSwap;
    }
}
