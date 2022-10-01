using UnityEngine;

public class FollowTransform : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private bool followPosition = true;
	[SerializeField] private bool followRotation = true;

	private void Update()
	{
		if (followPosition)
		{
			transform.position = target.position;
		}
		
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
