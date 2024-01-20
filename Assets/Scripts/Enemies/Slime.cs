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

    bool isMoving, alreadyDead;

    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (hitPoints > 0) base.Update();

        isMoving = base.agent.velocity.magnitude > 0 ? true : false;

        animator.SetBool("isMoving", isMoving);
        
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

        if (hitPoints <= 0 && !alreadyDead)
        {
            alreadyDead = true;
            StartCoroutine(Die());
            animator.SetTrigger("Die");
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(destroyWhenDeadDelay);

        Destroy(gameObject);
    }
}
