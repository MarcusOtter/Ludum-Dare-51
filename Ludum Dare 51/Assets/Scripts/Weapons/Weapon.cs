using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	[Header("Base weapon settings")]
	[SerializeField] protected float damage;
	[SerializeField] protected float fireDelayInSeconds;
	[SerializeField] protected float spread;
}
