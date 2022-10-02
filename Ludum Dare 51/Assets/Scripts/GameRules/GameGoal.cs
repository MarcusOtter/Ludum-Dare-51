using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MassMurder", menuName = "ScriptableObjects/GameGoals/Test", order = 1)]
public class GameGoal : ScriptableObject
{
    public string _goalName = "Goal";
    public string _goalDescription = "Get points!";
    public AudioClip _entrySoundEffect;
    public Color _entryColor = Color.white;

    public virtual void OnEnter()
    {
        Debug.Log($"Subscribe {_goalName} events!");
    }
    public virtual void OnExit()
    {
        Debug.Log($"Unsubscribe {_goalName} events!");
    }

}
