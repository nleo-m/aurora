using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (PlayerState.isDead) return;

        if (Input.GetButtonUp("Fire1")) PlayerState.attackCounter++;

        PlayerState.isDefending = Input.GetButton("Fire2");
    }
}
