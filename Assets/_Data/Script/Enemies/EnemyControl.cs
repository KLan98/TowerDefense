using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : Load
{
    protected NavMeshAgent agent;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadNavMeshAgent();
        AssignAgentToMoving();
    }

    protected virtual void LoadNavMeshAgent()
    {
        if (this.agent != null) return;
        this.agent = GetComponent<NavMeshAgent>();
    }

    private void AssignAgentToMoving()
    {
        EnemyMoving enemyMoving = GetComponent<EnemyMoving>();
        if (enemyMoving != null)
        {
            enemyMoving.agent = this.agent;
        }
    }
}