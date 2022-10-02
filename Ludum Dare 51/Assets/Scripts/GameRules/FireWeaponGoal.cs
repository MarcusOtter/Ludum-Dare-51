using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireWeapon", menuName = "ScriptableObjects/GameGoals/FireWeapon", order = 1)]
public class FireWeaponGoal : GameGoal
{
    public int PointsPerShot = 1;
    public void GainPointsForShooting(Weapon _)
    {
        GameManager.AddPoints(PointsPerShot);
    }

    public override void OnEnter()
    {
        PlayerWeapon.OnBulletFired += GainPointsForShooting;
    }
    public override void OnExit()
    {
        PlayerWeapon.OnBulletFired -= GainPointsForShooting;
    }
}
