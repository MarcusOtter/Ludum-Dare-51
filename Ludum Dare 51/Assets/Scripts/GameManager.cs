using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public int score;
    [SerializeField] private List<GameGoal> _goals;
    public float goalChangeInterval = 10f; //Them's the rules!

    private GameGoal _currentGoal = null;
    public static Action OnScore;
    internal static GameManager Manager;

    public static bool PrintALot = true;



    private void Start()
    {
        if(Manager == null) Manager = this;
        StartCoroutine(CycleRules());
    }

    public static GameManager GetManager()
    {
        if(Manager == null)
        {
            Manager = new GameObject("GameManager").AddComponent<GameManager>();
        }
        return Manager;
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
        foreach(var goal in _goals)
        {
            _currentGoal = goal;
            _currentGoal?.OnEnter();
            yield return new WaitForSeconds(goalChangeInterval);
            _currentGoal?.OnExit();
        }

        print("Game End!");

        yield return null;
    }
}
