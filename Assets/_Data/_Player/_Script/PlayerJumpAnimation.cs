using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpAnimation : PlayerAbstract
{
    private void FixedUpdate()
    {
        PlayerJumpAnimationSet();
    }

    public void PlayerJumpAnimationSet()
    {
        this.playerAnimator.SetBool(Const.PLAYER_IS_GROUNDED, thirdPersonMovement.IsGrounded());
    }

    protected override void LoadPlayerAnimator()
    {
        playerAnimator = transform.parent.GetComponentInChildren<Animator>();
    }

    protected override void LoadThirdPersonMovement()
    {
        thirdPersonMovement = transform.parent.GetComponent<ThirdPersonMovement>();
    }
}
