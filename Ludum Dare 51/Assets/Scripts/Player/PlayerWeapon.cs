using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform weaponHolder;

	[Header("Settings")]
	[SerializeField] private float dropDistance = 0.5f;
	
	private PlayerInput _playerInput;
	private Weapon _currentWeapon;

	private readonly Dictionary<int, Weapon> _weaponsInSwapRange = new();
	
	private void Awake()
	{
		_playerInput = FindObjectOfType<PlayerInput>();
	}

	private void OnEnable()
	{
		_playerInput.OnSwapWeaponKeyDown += SwapWeapon;
	}

	private void OnDisable()
	{
		_playerInput.OnSwapWeaponKeyDown -= SwapWeapon;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.transform.root.TryGetComponent<Weapon>(out var weapon)) return;
		
		if (_currentWeapon == null)
		{
			PickUpWeapon(weapon);
			return;
		}

		if (_weaponsInSwapRange.ContainsKey(weapon.GetInstanceID()))
		{
			_weaponsInSwapRange[weapon.GetInstanceID()] = weapon;
		}
		else
		{
			_weaponsInSwapRange.Add(weapon.GetInstanceID(), weapon);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!other.transform.root.TryGetComponent<Weapon>(out var weapon)) return;
		_weaponsInSwapRange.Remove(weapon.GetInstanceID());
	}

	// ReSharper disable Unity.PerformanceAnalysis
	private void SwapWeapon()
	{
		var swappableWeapons = _weaponsInSwapRange.Values
			.Where(x => x.GetInstanceID() != _currentWeapon.GetInstanceID());
		
		if (!swappableWeapons.Any()) return;
		var closestWeapon = swappableWeapons
			.OrderBy(x => (x.transform.position - transform.root.position).sqrMagnitude)
			.First();

		if (_currentWeapon != null)
		{
			_currentWeapon.Drop(_currentWeapon.transform.position.Add(x: dropDistance, y: dropDistance));
		}
		
		PickUpWeapon(closestWeapon);
	}

	private void PickUpWeapon(Weapon weapon)
	{
		_weaponsInSwapRange.Remove(weapon.GetInstanceID());
		_currentWeapon = weapon;
		weapon.PickUp(weaponHolder);
	}
}
