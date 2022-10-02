using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
	public static Action<RangedWeapon> OnBulletFired;
	public static Action<Weapon> OnWeaponPickedUp;
	
	[Header("References")]
	[SerializeField] private Transform weaponHolder;

	[Header("Settings")]
	[SerializeField] private float dropDistance = 0.5f;
	
	private PlayerInput _playerInput;
	private Weapon _currentWeapon;

	private readonly Dictionary<int, Weapon> _weaponsInSwapRange = new();
	private bool _isHoldingAttack;
	
	private void Awake()
	{
		_playerInput = FindObjectOfType<PlayerInput>();
	}

	private void OnEnable()
	{
		_playerInput.OnSwapWeaponKeyDown += SwapWeapon;
		_playerInput.OnAttackKeyDown += HandleAttackKeyDown;
		_playerInput.OnAttackKeyUp += HandleAttackKeyUp;
	}

	private void OnDisable()
	{
		_playerInput.OnSwapWeaponKeyDown -= SwapWeapon;
		_playerInput.OnAttackKeyDown -= HandleAttackKeyDown;
		_playerInput.OnAttackKeyUp -= HandleAttackKeyUp;
	}

	private void Update()
	{
		if (_isHoldingAttack && _currentWeapon != null && _currentWeapon.isAutomatic)
		{
			_currentWeapon.Attack();
		}
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

	private void HandleAttackKeyUp()
	{
		_isHoldingAttack = false;
	}

	private void HandleAttackKeyDown()
	{
		_isHoldingAttack = true;
		if (_currentWeapon != null)
		{
			_currentWeapon.Attack();
		}
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
			_currentWeapon.OnAttack -= OnWeaponAttack;
			_currentWeapon.Drop(_currentWeapon.transform.position.Add(x: dropDistance, y: dropDistance));
		}
		
		PickUpWeapon(closestWeapon);
	}

	private void PickUpWeapon(Weapon weapon)
	{
		_weaponsInSwapRange.Remove(weapon.GetInstanceID());
		_currentWeapon = weapon;
		weapon.PickUp(weaponHolder);
		weapon.OnAttack += OnWeaponAttack;
		OnWeaponPickedUp?.Invoke(weapon);
	}

	private static void OnWeaponAttack(Weapon weapon)
	{
		if (weapon is RangedWeapon rangedWeapon)
		{
			OnBulletFired?.Invoke(rangedWeapon);
		}
	}
}
