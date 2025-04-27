using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    bool attack;

    public AttackState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleInput()
    {
    }

    // UPDATE STATE TRANSITION
    public override void LogicUpdate()
    {
        player.animator.SetTrigger(Const.PLAYER_ATTACK);

        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            //Debug.Log("Landing: " + landing);
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                attack = false;
                //Debug.Log("Landing: " + landing);
            }

            else
            {
                attack = true;
            }
        }

        else
        {
            attack = true;
        }

        if (!attack)
        {
            player.animator.ResetTrigger(Const.PLAYER_ATTACK);
            player.animator.SetTrigger(Const.PLAYER_MOVE);
            stateMachine.ChangeState(player.sprinting);
        }
    }

    public override void PhysicsUpdate()
    {

    }
}

