using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    private Animator _animator;
    private Weapon _currentWeapon;
    private int _currentWeaponIndex = 0;
    private int _currentHealth;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        ChangeWeapon(_weapons[_currentWeaponIndex]);
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if(_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
        _weapons.Add(weapon);
    }

    public void NextWeapon()
    {
        if(_currentWeaponIndex == _weapons.Count - 1)
        {
            _currentWeaponIndex = _weapons.Count - 1;
        }
        else
        {
            _currentWeaponIndex++;
        }

        ChangeWeapon(_weapons[_currentWeaponIndex]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponIndex == 0)
        {
            _currentWeaponIndex = _weapons.Count - 1;
        }
        else
        {
            _currentWeaponIndex--;
        }

        ChangeWeapon(_weapons[_currentWeaponIndex]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            _currentWeapon.Shoot(_shootPoint);
        }
    }

    private void OnEnemyDied(int reward)
    {
        Money += reward;
    }
}