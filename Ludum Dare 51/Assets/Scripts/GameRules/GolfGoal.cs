using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Golf", menuName = "ScriptableObjects/GameGoals/Golf", order = 1)]
public class GolfGoal : GameGoal
{
    [SerializeField] private Transform _golfCourse;
    [SerializeField] private Weapon _golfClub;
    private EnvironmentManager environment;
    public override void OnEnter()
    {
        environment = FindObjectOfType<EnvironmentManager>();
        environment.StartGolf();
    }
    public override void OnExit()
    {
        environment.EndGolf();
    }
}
