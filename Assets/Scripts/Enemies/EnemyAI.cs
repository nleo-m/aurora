using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    Vector3 playerLastKnownPosition;
    bool chasingPlayer;

    [SerializeField] LayerMask whatIsGround, whatIsPlayer;

    Vector3 walkPoint;
    bool walkPointSet;

    [SerializeField] float attackDelay;
    protected bool alreadyAttacked;

    [SerializeField] float sightRange, attackRange, walkPointRange;
    [SerializeField] float minPatrolDelay, maxPatrolDelay;
    protected bool playerInSightRange, playerInAttackRange;

    Coroutine currentWalkPointCoroutine = null;

    protected virtual void Start()
    {
        player = GameObject.Find("Luna").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();


    }

    IEnumerator SearchWalkPoint()
    {
        walkPointSet = true;

        yield return new WaitForSeconds(Random.Range(minPatrolDelay, maxPatrolDelay));

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, 0f, transform.position.y + randomZ);
        currentWalkPointCoroutine = null;
    }

    void ClearWalkPoint()
    {
        chasingPlayer = false;
        walkPointSet = false;
    }


    protected void Patrol()
    {
        if (chasingPlayer) walkPoint = playerLastKnownPosition;

        if (!walkPointSet) currentWalkPointCoroutine = StartCoroutine(SearchWalkPoint());

        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 5 && currentWalkPointCoroutine == null) ClearWalkPoint();
    }

    protected void ChasePlayer()
    {
        chasingPlayer = true;
        playerLastKnownPosition = player.position;

        agent.SetDestination(player.position);
    }

    protected virtual void AttackPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        float angleDifference = Quaternion.Angle(transform.rotation, lookRotation) * Mathf.Deg2Rad;

        if (angleDifference > 0.3f) transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Attack();
            alreadyAttacked = true;
            StartCoroutine(ResetAttack());
        }
    }

    protected abstract void Attack();

    protected IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackDelay);

        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
