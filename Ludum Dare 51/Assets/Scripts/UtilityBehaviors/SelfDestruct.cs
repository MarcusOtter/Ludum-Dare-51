using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
	[SerializeField] private float timeToLive = 10f;
	[SerializeField] private bool destroyParent;
	
	private void Awake()
	{
		var obj = destroyParent ? transform.parent.gameObject : gameObject;
		Destroy(obj, timeToLive);
	}
}

