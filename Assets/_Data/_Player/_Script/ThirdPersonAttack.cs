using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonAttack : Load
{
    private PlayerInputActions playerActionAsset; // Stores input action map generated from Input System
    private void FixedUpdate()
    {
        //PlayerLightAttack();
        //PlayerHeavyAttack();
    }

    private void PlayerLightAttack()
    {

    }

    private void OnEnable()
    {
        //playerActionAsset.Player.Attack.started += DoAttack;
    }

    private void OnDisable()
    {
        //playerActionAsset.Player.Attack.started -= DoAttack;
    }

    //private void DoAttack(InputAction.CallbackContext context)
    //{

    //    Debug.Log("Attack");
    //}
}
