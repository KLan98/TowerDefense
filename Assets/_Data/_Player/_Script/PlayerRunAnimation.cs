using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunAnimation : PlayerAbstract
{
    private void FixedUpdate()
    {
        PlayerMovingTransitions();
    }

    protected virtual void PlayerMovingTransitions()
    {
        // current speed of the player
        float speed = thirdPersonMovement.Rb.velocity.magnitude;
        // Debug.Log("Player speed :" + speed);
        float maxSpeed = 12f;

        this.playerAnimator.SetFloat(Const.PLAYER_MOVING_TRANSITIONS, speed/ maxSpeed);
    }

    protected override void LoadThirdPersonMovement()
    {
        thirdPersonMovement = transform.parent.GetComponent<ThirdPersonMovement>();
    }

    protected override void LoadPlayerAnimator()
    {
        playerAnimator = transform.parent.GetComponentInChildren<Animator>();
    }
}
