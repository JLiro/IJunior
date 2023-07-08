using UnityEngine;

public static class PlayerAnimator
{
    public static readonly int Run = Animator.StringToHash(nameof(Run));
    public static readonly int Idle = Animator.StringToHash(nameof(Idle));
}