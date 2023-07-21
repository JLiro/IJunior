using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue;

    private int _currentHealth;

    public event UnityAction<int, int> Changed;

    private void Awake()
    {
        _currentHealth = _maxValue;
    }

    public void Add(int health)
    {
        _currentHealth += health;

        _currentHealth = Mathf.Min(_currentHealth, _maxValue);

        Changed?.Invoke(_currentHealth, _maxValue);
    }

    public void Remove(int health)
    {
        _currentHealth -= health;

        _currentHealth = Mathf.Max(_currentHealth, 0);

        Changed?.Invoke(_currentHealth, _maxValue);
    }
}