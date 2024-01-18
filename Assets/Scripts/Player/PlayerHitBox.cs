using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        int attackPower = 1 * PlayerState.attackCounter;

        if (damageable != null )
        {
            damageable.TakeDamage(attackPower);
        }
    }
}