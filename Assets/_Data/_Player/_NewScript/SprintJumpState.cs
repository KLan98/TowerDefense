using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// sprint and then jump
public class SprintJumpState : State
{
    bool sprintJump;
    bool landing;

    public SprintJumpState(Player player, StateMachine stateMachine) : base(player, stateMachine)  
    {

    }

    public override void Enter()
    {
        base.Enter();

        player.animator.applyRootMotion = true; 
    }

    public override void HandleInput()
    {
    }

    // UPDATE STATE TRANSITION
    public override void LogicUpdate()
    {
        player.animator.SetTrigger(Const.PLAYER_SPRINT_JUMP);

        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("SprintJump"))
        {
            //Debug.Log("Landing: " + landing);
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                sprintJump = false;
                //Debug.Log("Landing: " + landing);
            }

            else
            {
                sprintJump = true;
            }
        }

        else
        {
            sprintJump = true;
        }

        if (!sprintJump)
        {
            player.animator.ResetTrigger(Const.PLAYER_SPRINT_JUMP);
            player.animator.SetTrigger(Const.PLAYER_MOVE);
            player.animator.applyRootMotion = false;
            stateMachine.ChangeState(player.sprinting);
        }
    }

    public override void PhysicsUpdate()
    {
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
