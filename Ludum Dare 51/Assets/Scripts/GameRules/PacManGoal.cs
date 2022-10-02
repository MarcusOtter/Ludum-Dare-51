using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pacman", menuName = "ScriptableObjects/GameGoals/Pacman", order = 1)]
public class PacManGoal : GameGoal
{
    private EnvironmentManager environment;
    public override void OnEnter()
    {
        environment = FindObjectOfType<EnvironmentManager>();
        environment?.StartPacMan();
    }
    public override void OnExit()
    {
        environment?.EndPacMan();
    }
}
