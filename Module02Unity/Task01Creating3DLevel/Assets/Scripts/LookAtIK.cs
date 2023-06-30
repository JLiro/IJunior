using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class LookAtIK : MonoBehaviour
{
    protected Animator Animator;
    public bool IkActive = false;
    public Transform Look0bj = null;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
    
    private void OnAnimatorIK()
    {
        if (Animator)
            if (IkActive)
                if (Look0bj != null)
                {
                    Animator.SetLookAtWeight(1);
                    Animator.SetLookAtPosition(Look0bj.position);
                }
        else Animator.SetLookAtWeight(0);
    }
}