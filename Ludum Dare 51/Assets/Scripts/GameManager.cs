using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameGoal _currentGoal = null;
    [SerializeField] private List<GameGoal> _goals;
    public int score;
    public float goalChangeInterval = 10f; //Them's the rules!
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
