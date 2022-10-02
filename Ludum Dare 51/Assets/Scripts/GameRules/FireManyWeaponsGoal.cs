using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireDifferentWeapons", menuName = "ScriptableObjects/GameGoals/FireDifferentWeapon", order = 1)]
public class FireManyWeaponsGoal : GameGoal
{
    public int PointsPerWeaponShot = 15;
    private List<int> _firedIDs;

    public void GainPointsForShooting(Weapon firedWeapon)
    {
        if(!_firedIDs.Contains(firedWeapon.GetInstanceID()))
        {
            _firedIDs.Add(firedWeapon.GetInstanceID());
            GameManager.AddPoints(PointsPerWeaponShot);
        }
    }

    public override void OnEnter()
    {
        _firedIDs.Clear();
        PlayerWeapon.OnBulletFired += GainPointsForShooting;
    }
    public override void OnExit()
    {
        PlayerWeapon.OnBulletFired -= GainPointsForShooting;
    }
}

