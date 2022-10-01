using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameGoal _currentGoal = null;
    [SerializeField] private List<GameGoal> _goals;
    public int points;
    public float goalChangeInterval = 10f; //Them's the rules!
    internal static GameManager Manager;

    private void Start()
    {
        StartCoroutine(CycleRules());
    }

    public static int GetPoints()
    {
        return Manager.points;
    }

    public IEnumerator CycleRules()
    {
        foreach(var goal in _goals)
        {
            _currentGoal = goal;
            _currentGoal.OnEnter();
            yield return new WaitForSeconds(goalChangeInterval);
            _currentGoal?.OnExit();
        }

        print("Game End!");

        yield return null;
    }
}
