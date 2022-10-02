using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(FollowTransform))]
public abstract class Weapon : MonoBehaviour
{
	[Header("Base weapon settings")]
	[SerializeField] protected float damage;
	[SerializeField] protected float fireDelayInSeconds;
	[SerializeField] protected float spread;

	protected Rigidbody Rigidbody;
	
	private FollowTransform _followTransform;
	
	protected virtual void Awake()
	{
		Rigidbody = GetComponent<Rigidbody>();
		_followTransform = GetComponent<FollowTransform>();
	}

	public virtual void PickUp(Transform weaponHolderTransform)
	{
		_followTransform.SetTarget(weaponHolderTransform);
		Rigidbody.isKinematic = true;
	}

	public virtual void Drop(Vector3 newPosition)
	{
		transform.position = newPosition;
		_followTransform.SetTarget(null);
		Rigidbody.isKinematic = false;
	}
}
