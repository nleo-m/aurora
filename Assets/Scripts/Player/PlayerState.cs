using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : Singleton<PlayerState>, IDamageable
{
    public static bool isWalking { get; set; }
    public static bool isDefending { get; set; }
    public static bool isDead { get; set; }

    public static float movementSpeed { get; set; } = 4.5f;

    public static int attackCounter { get; set; } = 0;
    static int previousAttackCounter = attackCounter;
    static int maxAttackCounter = 4;

    static Coroutine currentClearAttackCoroutine = null;

    [SerializeField] int hitPoints = 30;

    private void Update()
    {
        if (isDead) return;

        if (hitPoints <= 0) isDead = true;

        if (previousAttackCounter > 0 || attackCounter > 0)
        {
            if (currentClearAttackCoroutine == null) currentClearAttackCoroutine = StartCoroutine(ClearAttackCounterCoroutine(1));

            if (attackCounter > previousAttackCounter) ClearCoroutine(currentClearAttackCoroutine);
        }

        if (attackCounter > maxAttackCounter) attackCounter = 1;

        if (isDefending) attackCounter = 0;

        movementSpeed = isDefending ? 1.75f : 4.5f;

        previousAttackCounter = attackCounter;

    }

    IEnumerator ClearAttackCounterCoroutine(float secs)
    {
        yield return new WaitForSeconds(secs);

        ClearAttackCounter();
    }

    public static void ClearAttackCounter()
    {
        attackCounter = 0;
        currentClearAttackCoroutine = null;
    }

    void ClearCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
        currentClearAttackCoroutine = null;
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
    }
}
