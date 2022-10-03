using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public static int PreviousScore = -1;
    public static int BestScore = -1;
    public int Score;
    public List<GameGoal> Goals;
    public float GoalChangeInterval = 10f; //Them's the rules!
    public float LastChangeTime;

    internal GameGoal CurrentGoal;
    public static Action OnScore;
    public static Action<GameGoal> OnNewGoal;

    public static bool PrintALot = false;
    
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
            SoundEffectPlayer.PlaySoundEffect(CurrentGoal?.EntrySoundEffect, transform);
            LastChangeTime = Time.time;
            OnNewGoal?.Invoke(CurrentGoal);
            yield return new WaitForSeconds(GoalChangeInterval);
            CurrentGoal?.OnExit();
        }

        print("Game End!");

        yield return null;
    }
}
