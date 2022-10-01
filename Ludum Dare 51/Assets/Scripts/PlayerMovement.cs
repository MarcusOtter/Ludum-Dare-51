using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float movementSpeed;

	private PlayerInput _input;
	private Rigidbody _rigidbody;

	
	private void Start()
	{
		_input = FindObjectOfType<PlayerInput>();
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		_rigidbody.velocity = 
			Vector3.ClampMagnitude(new Vector3(_input.HorizontalAxis, 0, _input.VerticalAxis), 1) 
			* movementSpeed;

		RotateTowardsMouse();
	}
	
	private void RotateTowardsMouse()
	{
		transform.forward = (_input.MouseWorldPosition - transform.position).normalized;
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.With(x: 0));
	}
}

