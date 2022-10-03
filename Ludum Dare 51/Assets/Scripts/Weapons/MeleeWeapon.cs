using UnityEngine;

public class MeleeWeapon : Weapon
{
	[Header("Melee weapon settings")]
	[SerializeField] private GameObject attackCollider;
	[SerializeField] private float attackDuration = 0.5f;
	[SerializeField] private float attackDelay;

	public override void Attack()
	{
		if (base.CanAttack())
		{
			this.FireAndForgetWithDelay(attackDelay, () => this.attackCollider.SetActive(true));
			this.FireAndForgetWithDelay(attackDelay + attackDuration, () => attackCollider.SetActive(false));
		}
		
		base.Attack();
	}
}
