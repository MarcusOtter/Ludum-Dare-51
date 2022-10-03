using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Golf", menuName = "ScriptableObjects/GameGoals/Golf", order = 1)]
public class GolfGoal : GameGoal
{
    [SerializeField] private Transform _golfCourse;
    [SerializeField] private Weapon _golfClub;
    private EnvironmentManager environment;
    public static Action OnGolfStart;
    public static Action OnGolfEnd;
    public override void OnEnter()
    {
        OnGolfStart?.Invoke();
        environment = FindObjectOfType<EnvironmentManager>();
        environment.StartGolf();
    }
    public override void OnExit()
    {
        environment.EndGolf();
        OnGolfEnd?.Invoke();
    }
}
