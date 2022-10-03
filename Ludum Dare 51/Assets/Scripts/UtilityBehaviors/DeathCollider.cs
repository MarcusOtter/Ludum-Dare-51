using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeathCollider : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		var hurtable = other.transform.root.GetComponentInChildren<IHurtable>();
		if (hurtable != null)
		{
			hurtable.TakeDamage(9999999);
		}

		Destroy(other);
	}
}
