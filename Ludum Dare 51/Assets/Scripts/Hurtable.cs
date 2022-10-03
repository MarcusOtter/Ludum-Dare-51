using System.Linq;
using UnityEngine;

public class Hurtable : MonoBehaviour, IHurtable
{
	[SerializeField] private int maxHealth;
	[SerializeField] private SoundEffect hurtSound;
	[SerializeField] private SoundEffect deathSound;
	[SerializeField] private GameObject[] alwaysSpawnOnDeath;
	[SerializeField] private GameObject[] randomObjectSpawnOnDeath;
	[SerializeField] private int amountOfLootToSpawn = 1;
	[SerializeField] private Color hurtParticleColor;

	private static readonly int DamageAnimationTrigger = Animator.StringToHash("TakeDamage");
	private static readonly int DeathAnimationTrigger = Animator.StringToHash("Die");

	private Animator _animator;
	
	private int _currentHealth;

	private void Awake()
	{
		_currentHealth = maxHealth;
		_animator = GetComponentInChildren<Animator>();
	}

	public Color GetHurtParticleColor() => hurtParticleColor;
	
	public bool IsLethalDamage(int damage) => _currentHealth - damage <= 0;
	
	public void TakeDamage(int damage)
	{
		_currentHealth -= damage;
		SoundEffectPlayer.PlaySoundEffect(hurtSound, transform);
		if (_animator != null)
		{
			_animator.SetTrigger(DamageAnimationTrigger);
		}

		if (_currentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		SoundEffectPlayer.PlaySoundEffect(deathSound, transform);
		if (_animator != null)
		{
			_animator.SetTrigger(DeathAnimationTrigger);
		}
		
		foreach(var obj in alwaysSpawnOnDeath)
		{
			Instantiate(obj, transform.position, Quaternion.identity);
		}
		
		if (randomObjectSpawnOnDeath.Any())
		{
			for (var i = 0; i < amountOfLootToSpawn; i++)
			{
				var obj = randomObjectSpawnOnDeath[Random.Range(0, randomObjectSpawnOnDeath.Length)];
				Instantiate(obj, transform.position, Quaternion.identity);
			}
		}
		
		Destroy(gameObject);
	}
}

