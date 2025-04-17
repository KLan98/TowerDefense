using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantMoving : EnemyMoving
{
    protected override void ResetValue()
    {
        this.pathIndex = 0;
    }

    protected override void CheckMoving()
    {
        //Debug.Log("Velocity: "+ this.enemyControl.Agent.velocity.magnitude);
        if (this.enemyControl.Agent.velocity.magnitude > 0)
        {
            this.isMoving = true;
            //Debug.Log(isMoving);
        }

        else if (this.enemyControl.Agent.velocity.magnitude == 0)
        {
            this.isMoving = false;
            //Debug.Log(isMoving);
        }

        // set parameter to value of isMoving in animator
        this.enemyControl.Animator.SetBool(Const.IS_MOVING, this.isMoving);
    }
}
