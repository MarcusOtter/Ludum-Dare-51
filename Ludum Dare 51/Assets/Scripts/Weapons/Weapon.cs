using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(FollowTransform))]
public abstract class Weapon : MonoBehaviour
{
	[Header("Base weapon settings")]
	[SerializeField] protected int damage = 1;
	[SerializeField] protected float fireDelayInSeconds = 0.5f;
	[SerializeField] public bool isAutomatic;
	[SerializeField] protected SoundEffect attackSound;

	public Action<Weapon> OnAttack;
	protected Rigidbody Rigidbody;
	
	private FollowTransform _followTransform;

	private float _lastFireTime;
	
	protected virtual void Awake()
	{
		Rigidbody = GetComponent<Rigidbody>();
		_followTransform = GetComponent<FollowTransform>();
	}

	public virtual void Attack()
	{
		if (!CanAttack()) return;
		
		SoundEffectPlayer.PlaySoundEffect(attackSound, transform);
		_lastFireTime = Time.time;
		OnAttack?.Invoke(this);
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
	
	protected virtual bool CanAttack()
	{
		return Time.time >= _lastFireTime + fireDelayInSeconds;
	}
}
