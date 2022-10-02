using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public int score;
    public List<GameGoal> goals;
    public float goalChangeInterval = 10f; //Them's the rules!

    internal GameGoal _currentGoal = null;
    public static Action OnScore;
    public static Action OnNewGoal;
    internal static GameManager Manager;

    public static bool PrintALot = true;



    private void Start()
    {
        Manager = GetManager();
        StartCoroutine(CycleRules());
    }

    public static GameManager GetManager()
    {
        if (Manager == null)
        {
            Manager = FindObjectOfType<GameManager>();
        }
        if (Manager == null)
        {
            Manager = new GameObject("GameManager").AddComponent<GameManager>();
        }
        return Manager;
    }

    public static string GetCurrentGoalName()
    {
        if (GetManager()._currentGoal)
        {
            return GetManager()._currentGoal._goalName;
        }
        return "";
    }

    public static string GetCurrentGoalDescription()
    {
        if (GetManager()._currentGoal)
        {
            return GetManager()._currentGoal._goalDescription;
        }
        return "";
    }

    public static int GetPoints()
    {
        return GetManager().score;
    }

    public static void AddPoints(int points)
    {
        GetManager().score += points;
        OnScore?.Invoke();
    }

    public IEnumerator CycleRules()
    {
        foreach (var goal in goals)
        {
            _currentGoal = goal;
            _currentGoal?.OnEnter();
            OnNewGoal?.Invoke();
            yield return new WaitForSeconds(goalChangeInterval);
            _currentGoal?.OnExit();
        }

        print("Game End!");

        yield return null;
    }
}
