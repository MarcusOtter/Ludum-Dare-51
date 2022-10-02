using UnityEngine;

public class Rotate : MonoBehaviour
{
	[SerializeField] private float speed = 1f;
	[SerializeField] private Vector3 axis = Vector3.up;
	
	private void Update ()
	{
		transform.Rotate(axis, speed * Time.deltaTime);
	}
}

