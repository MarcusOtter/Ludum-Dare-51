using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExplosionTrigger : MonoBehaviour
{
	[SerializeField] private float explosionForce = 10f;
	[SerializeField] private Transform explosionCenter;
	[SerializeField] private float explosionUpwardModifier = 1f;
	[SerializeField] private int damage = 5;

	private void OnTriggerEnter(Collider other)
	{
		// if (other.CompareTag("Player")) return;
		if (other.isTrigger) return;	

		if (other.attachedRigidbody != null)
		{
			other.attachedRigidbody.AddExplosionForce(explosionForce, explosionCenter.position, 0, explosionUpwardModifier, ForceMode.Impulse);
		}

		var hurtable = other.transform.root.GetComponentInChildren<IHurtable>();
		if (hurtable == null) return;
		
		hurtable.TakeDamage(damage);
	}
}
