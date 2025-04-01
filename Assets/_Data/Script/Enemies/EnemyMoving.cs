using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// control every aspects of moving an enemy 
public class EnemyMoving : Load
{
    [SerializeField] GameObject target;
    [SerializeField] EnemyControl enemyControl;

    void FixedUpdate()
    {
        this.MoveToTarget();
    }

    protected override void LoadComponent()
    {
        this.LoadEnemyControl();
        this.LoadTarget();
    }

    protected virtual void LoadEnemyControl()
    {
        if (enemyControl != null) return;
        enemyControl = transform.parent.GetComponent<EnemyControl>();
    }

    protected virtual void LoadTarget()
    {
        if (target != null) return;
        target = GameObject.Find("Target");
    }

    protected virtual void MoveToTarget()
    {
        this.enemyControl.Agent.SetDestination(target.transform.position);
    }
}
