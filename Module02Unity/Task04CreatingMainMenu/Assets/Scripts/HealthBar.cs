using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthBar : Bar
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>(); 
    }

    private void OnEnable()
    {
        _health.Changed += OnValueChanged;
    }

    private void OnDisable()
    {
        _health.Changed -= OnValueChanged;
    }
}