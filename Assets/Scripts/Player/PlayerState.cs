using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : Singleton<PlayerState>
{
    public static bool isWalking { get; set; }
    public static int attackCounter { get; set; } = 0;
    static int previousAttackCounter = attackCounter;
    static int maxAttackCounter = 4;

    static Coroutine currentClearAttackCoroutine = null;

    private void Update()
    {
        if (previousAttackCounter > 0 || attackCounter > 0)
        {
            if (currentClearAttackCoroutine == null) currentClearAttackCoroutine = StartCoroutine(ClearAttackCounterCoroutine(1.5f));

            if (attackCounter > previousAttackCounter) ClearCoroutine(currentClearAttackCoroutine);
        }

        if (attackCounter > maxAttackCounter)
        {
             ClearAttackCounter();
        }

        previousAttackCounter = attackCounter;
    }

    IEnumerator ClearAttackCounterCoroutine(float secs)
    {
        yield return new WaitForSeconds(secs);

        ClearAttackCounter();
    }

    void ClearAttackCounter()
    {
        attackCounter = 0;
        currentClearAttackCoroutine = null;
    }

    void ClearCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
        currentClearAttackCoroutine = null;
    }
}
