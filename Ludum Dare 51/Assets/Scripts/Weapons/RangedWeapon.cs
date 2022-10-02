using UnityEngine;

public class RangedWeapon : Weapon
{
	[Header("Ranged weapon settings")]
	[SerializeField] private Transform shellCasingSpawnPoint;
	[SerializeField] private GameObject shellCasingPrefab;
	[SerializeField] private Transform bulletSpawnPoint;
	[SerializeField] private Bullet bulletPrefab;
	[SerializeField] private float bulletSpeed = 5f;
	[SerializeField] [Range(0f, 1f)] protected float accuracy = 1f;
	
	public override void Attack()
	{
		if (!CanAttack()) return;
		
		base.Attack();
		var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, GetRandomBulletOffset());
		bullet.Shoot(damage, bulletSpeed);
	}
	
	/// <summary>
	/// Calculates a rotation with a random offset that depends on the accuracy
	/// </summary>
	private Quaternion GetRandomBulletOffset()
	{
		// If the accuracy is 1, the bullet will always go in a straight line.
		if (Mathf.Approximately(accuracy, 1f))
		{
			return bulletSpawnPoint.rotation;
		}

		// With 0 accuracy, the offset can be between -45 degrees and + 45 degrees.
		// With 0.5 accuracy, the offset can be between -22.5 degrees and + 22.5 degrees.
		// (etc)
		var randomOffsetDegrees = Random.Range((1 - accuracy) * -45, (1 - accuracy) * 45);
		return Quaternion.AngleAxis(randomOffsetDegrees, Vector3.up) * bulletSpawnPoint.rotation;
	}
}

