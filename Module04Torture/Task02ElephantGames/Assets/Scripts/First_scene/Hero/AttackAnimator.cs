using UnityEngine;

public class AttackAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnMouseDown()
    {
        _animator.Play("Attack");

        Invoke("ReturnToIdle", _animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void ReturnToIdle()
    {
        _animator.Play("Idle");
    }
}