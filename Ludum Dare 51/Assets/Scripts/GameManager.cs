using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public int Score;
    public List<GameGoal> Goals;
    public float GoalChangeInterval = 10f; //Them's the rules!
    public float LastChangeTime;

    internal GameGoal CurrentGoal = null;
    public static Action OnScore;
    public static Action<GameGoal> OnNewGoal;

    public static bool PrintALot = true;
    
    private void Start()
    {
        StartCoroutine(CycleRules());
    }

    public static int GetPoints()
    {
        return Instance.Score;
    }

    public static void AddPoints(int points)
    {
        Instance.Score += points;
        OnScore?.Invoke();
    }

    private IEnumerator CycleRules()
    {
        foreach (var goal in Goals)
        {
            CurrentGoal = goal;
            CurrentGoal?.OnEnter();
            LastChangeTime = Time.time;
            OnNewGoal?.Invoke(CurrentGoal);
            yield return new WaitForSeconds(GoalChangeInterval);
            CurrentGoal?.OnExit();
        }

        print("Game End!");

        yield return null;
    }
}
