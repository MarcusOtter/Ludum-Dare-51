using UnityEngine;

[CreateAssetMenu(fileName = "End", menuName = "ScriptableObjects/GameGoals/SuicideGoal", order = 1)]
public class SuicideGoal : GameGoal
{
	[SerializeField] private int pointsPerSecond;
	
	public override void OnEnter()
	{
		base.OnEnter();
		PlayerMovement.OnPlayerDeath += GetPoints;
	}

	private void GetPoints()
	{
		var timeRemaining = 10 - (Time.time - GameManager.Instance.LastChangeTime);
		var pointsToAdd = (int)(pointsPerSecond * timeRemaining);
		if (pointsToAdd > 0)
		{
			GameManager.AddPoints(pointsToAdd);
		}
	}

	public override void OnExit()
	{
		base.OnExit();
		PlayerMovement.OnPlayerDeath -= GetPoints;
	}
}
