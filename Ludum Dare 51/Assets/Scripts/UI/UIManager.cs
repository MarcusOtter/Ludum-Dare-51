using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.OnNewGoal += UpdateGoalName;
        GameManager.OnNewGoal += UpdateGoalDescription;
    }
    private void OnDisable()
    {
        GameManager.OnNewGoal -= UpdateGoalName;
        GameManager.OnNewGoal -= UpdateGoalDescription;
    }

    private void UpdateGoalName()
    {

    }
    private void UpdateGoalDescription()
    {

    }
}
