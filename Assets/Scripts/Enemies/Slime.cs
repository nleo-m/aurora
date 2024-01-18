using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Slime : EnemyAI, IDamageable
{
    Animator animator;
    [SerializeField] Collider damageZone;
    [SerializeField] int hitPoints;
    [SerializeField] int destroyWhenDeadDelay;

    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (hitPoints > 0) base.Update();
        
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

        if (hitPoints <= 0)
        {
            animator.SetTrigger("Die");
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(destroyWhenDeadDelay);

        Destroy(gameObject);
    }
}
