using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : Hurtable
{
	[SerializeField] private float movementSpeed;
	[SerializeField] private float deathDelay = 3f;

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

	public override void Die(float delay = 0)
	{
		GameManager.PreviousScore = GameManager.Instance.Score;
		if (GameManager.PreviousScore > GameManager.BestScore)
		{
			GameManager.BestScore = GameManager.PreviousScore;
		}

		Camera.main.orthographicSize = 5f;
		
		this.FireAndForgetWithDelay(deathDelay, () => SceneManager.LoadScene(0));

		// Last hour strats
		{
			GetComponentInChildren<PlayerInput>().enabled = false;
			GetComponentInChildren<PlayerMovement>().enabled = false;
			GetComponentInChildren<Rigidbody>().isKinematic = true;
			foreach (var coll in GetComponentsInChildren<Collider>())
			{
				coll.enabled = false;
			}
			
			GetComponentInChildren<PlayerWeapon>().enabled = false;

			foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>())
			{
				meshRenderer.enabled = false;
			}
		}
		
		// Never destroy player :)
		base.Die(999f);
	}
	
	private void RotateTowardsMouse()
	{
		transform.forward = (_input.MouseWorldPosition - transform.position).normalized;
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.With(x: 0));
	}
}

