using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Slime : EnemyAI
{
    Animator animator;
    [SerializeField] Collider damageZone;
    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
