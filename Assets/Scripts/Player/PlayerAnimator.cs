using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (PlayerState.isDead) animator.SetTrigger("Die");

        animator.SetBool("isWalking", PlayerState.isWalking);

        animator.SetInteger("Attack", PlayerState.attackCounter);

        animator.SetBool("isDefending", PlayerState.isDefending);

        if (Input.GetButtonUp("Fire1")) animator.SetTrigger("AttackTrigger");
    }
}
