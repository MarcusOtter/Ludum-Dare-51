using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public int score;
    public List<GameGoal> goals;
    public float goalChangeInterval = 10f; //Them's the rules!

    internal GameGoal _currentGoal = null;
    public static Action OnScore;
    public static Action<GameGoal> OnNewGoal;

    public static bool PrintALot = true;
    
    private void Start()
    {
        StartCoroutine(CycleRules());
    }

    public static int GetPoints()
    {
        return Instance.score;
    }

    public static void AddPoints(int points)
    {
        Instance.score += points;
        OnScore?.Invoke();
    }

    private IEnumerator CycleRules()
    {
        foreach (var goal in goals)
        {
            _currentGoal = goal;
            _currentGoal?.OnEnter();
            OnNewGoal?.Invoke(_currentGoal);
            yield return new WaitForSeconds(goalChangeInterval);
            _currentGoal?.OnExit();
        }

        print("Game End!");

        yield return null;
    }
}
