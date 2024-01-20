using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField] LayerMask[] layersToDamage;

    public void OnTriggerEnter(Collider other)
    {
        int colliderLayer = other.gameObject.layer;

        if (CheckIfInLayersToDamage(colliderLayer))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            int attackPower = 1 * PlayerState.attackCounter;

            if (damageable != null)
            {
                damageable.TakeDamage(attackPower);
            }
        }
    }

    bool CheckIfInLayersToDamage(int colliderLayer)
    {
        foreach (LayerMask layer in layersToDamage)
        {
            if ((layer.value & (1 << colliderLayer)) != 0)
            {
                return true;
            }
        }

        return false;
    }
}

