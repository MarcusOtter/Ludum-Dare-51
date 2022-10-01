using UnityEngine;

public class FollowTransform : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private bool followRotation = true;

	private void Update()
	{
		transform.position = target.position;
		if (followRotation)
		{
			transform.rotation = target.rotation;
		}
	}
	
	public void SetTarget(Transform targetTransform)
	{
		target = targetTransform;
	}
}
