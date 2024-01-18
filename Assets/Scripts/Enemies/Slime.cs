using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Slime : EnemyAI, IDamageable
{
    Animator animator;
    [SerializeField] Collider damageZone;
    [SerializeField] int hitPoints;
    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void TakeDamage(int damage = 0)
    {
        animator.SetTrigger("Hit");

        hitPoints -= damage;

        Debug.Log($"Slime was hit, {hitPoints} hitPoints left!");
    }
}
