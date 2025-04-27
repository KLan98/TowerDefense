using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{
    bool jump;
    bool landing;

    public JumpingState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.animator.applyRootMotion = true;
    }

    // do nothing while jumping
    public override void HandleInput()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        // Debug.Log(sprint);

        // avoid double jump
        if (jumpAction.WasPerformedThisFrame())
        {
            return;
        }

        // don't allow moving while airbourne by taking no inputs
        if (moveInput.sqrMagnitude > 0.1)
        {
            return;
        }
    }

    public override void LogicUpdate()
    {
        player.animator.SetTrigger(Const.PLAYER_JUMP);

        // if jumping animation is finished then move to landing animation
        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            //Debug.Log("Landing: " + landing);
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                jump = false;
                landing = true;
                //Debug.Log("Landing: " + landing);
            }

            else
            {
                jump = true;
                landing = false;
            }
        }

        else
        {
            jump = true;
            landing = false;
        }

        if (landing && !jump)
        {
            stateMachine.ChangeState(player.landing);
        }
    }

    public override void PhysicsUpdate()
    {
        //player.rb.AddForce(Vector3.up * player.jumpForce, ForceMode.Impulse);

        // avoid rotating while airbourne
        if (!player.IsGrounded())
        {
            player.rb.constraints |= RigidbodyConstraints.FreezeRotation;

            player.jumpForce = 0f; // if not grounded then the jump force = 0f
        }

        else if (player.IsGrounded())
        {
            player.rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        }
    }
}
