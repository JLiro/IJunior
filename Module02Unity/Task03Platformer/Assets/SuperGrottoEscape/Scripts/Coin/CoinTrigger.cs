using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Coin))]
public class CoinTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _inside;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _inside?.Invoke();
        }
    }
}
