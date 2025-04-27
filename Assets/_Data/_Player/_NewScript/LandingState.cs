using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingState : State
{
    bool jump;
    bool landing;
    bool sprintJump;

    public LandingState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    // do nothing while landing
    public override void HandleInput()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        // Debug.Log(sprint);

        // avoid double jump
        if (jumpAction.WasPerformedThisFrame() && moveInput.sqrMagnitude < 0.1)
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
        player.animator.SetTrigger(Const.PLAYER_LAND);

        // if landing animation is finished then move to idle animation
        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerLand"))
        {
            //Debug.Log("Landing: " + landing);
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                jump = false;
                landing = false;
                //Debug.Log("Landing: " + landing);
            }

            else
            {
                jump = false;
                landing = true;
            }
        }

        else
        {
            jump = false;
            landing = true;
        }

        if (!landing && !jump)
        {
            player.animator.ResetTrigger(Const.PLAYER_LAND);
            player.animator.SetTrigger(Const.PLAYER_MOVE);
            player.animator.applyRootMotion = false;
            stateMachine.ChangeState(player.idle);
        }
    }

    public override void PhysicsUpdate()
    {
        // Add extra downward force to make falling feel snappier and less floaty
        // if the velocity is < 0 meaning the object is falling downward
        if (player.rb.velocity.y < 0f)
        {
            player.rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime * 1.5f;
        }
    }
}
