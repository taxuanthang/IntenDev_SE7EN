using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator;

    public void Awake()
    {
        if(animator == null) animator = GetComponentInChildren<Animator>();
    }

    public void UpdateAnimatorValues(float moveAmount)
    {
        animator.SetFloat("Blend", moveAmount);
    }
}
