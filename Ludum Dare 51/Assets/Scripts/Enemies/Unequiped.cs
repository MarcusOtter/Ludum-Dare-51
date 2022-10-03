using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unequiped : Node
{
    private TestEnemy guy;
    public Unequiped(TestEnemy enemy) : base(enemy)
    {
        guy = enemy;
    }
    public override NodeStatus Run()
    {
        return guy.HeldWeapon == null ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
    }
}
