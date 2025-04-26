using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SprintingState : State
{
    bool sprint;

    public SprintingState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        // sprint = false;
    }

    public override void HandleInput()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        // Debug.Log(sprint);

        // if not moving then not sprinting
        if (moveInput.sqrMagnitude < 0.1f || sprintAction.WasReleasedThisFrame())
        {
            sprint = false;
        }
        else 
        {
            sprint = true;
        }

        //if (jumpAction.triggered)
        //{
        //    sprintJump = true;

        //}
    }

    public override void LogicUpdate()
    {
        if (sprint)
        {
            player.animator.SetFloat(Const.PLAYER_MOVING_TRANSITIONS, player.rb.velocity.magnitude / player.maxSpeed + 1.5f);
        }
        else
        {
            stateMachine.ChangeState(player.idle);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (sprint)
        {
            player.movementForce = player.maxMovementForce;

        }
        else 
        {
            player.movementForce = player.defaultMovementForce;
        }
    }
}
