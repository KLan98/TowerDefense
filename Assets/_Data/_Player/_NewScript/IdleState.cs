using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class IdleState : State
{
    // uniqe values for this class
    bool jump;
    bool sprint;

    public IdleState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        // init values of states
        jump = false;
        sprint = false;
        moveInput = Vector2.zero;

        // reset all other triggers when entering idle state
        player.animator.ResetTrigger(Const.PLAYER_LAND);
        player.animator.ResetTrigger(Const.PLAYER_JUMP);
    }

    public override void HandleInput()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        // stationary jump
        if (jumpAction.WasPerformedThisFrame() && moveInput.sqrMagnitude < 0.1)
        {
            jump = true;
            //Debug.Log("jump = true");
            player.jumpForce = player.allowedJumpForce;
        }

        else
        {
            jump = false;
            player.jumpForce = 0f;
        }

        if (sprintAction.WasPerformedThisFrame() && moveInput.sqrMagnitude > 0.1)
        {
            sprint = true;
        }
    }

    public override void LogicUpdate()
    {
        // set speed parameter
        // Debug.Log(player.rb.velocity.magnitude);
        player.animator.SetFloat(Const.PLAYER_MOVING_TRANSITIONS, player.rb.velocity.magnitude / player.maxSpeed);
        //Debug.Log(player.rb.velocity.magnitude / player.maxSpeed);

        if (sprint)
        {
            stateMachine.ChangeState(player.sprinting);
        }

        if (jump)
        {
            stateMachine.ChangeState(player.jumping);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.movementForce = player.defaultMovementForce;
    }

    public override void Exit()
    {
        base.Exit();

    }
}
