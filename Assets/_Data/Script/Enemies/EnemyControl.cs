using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// use for loading navmesh agent 
public class EnemyControl : Load
{
    protected NavMeshAgent agent;
    public NavMeshAgent Agent => agent;

    protected override void LoadComponent()
    {
        LoadNavMeshAgent();
    }

    protected virtual void LoadNavMeshAgent()
    {
        if (this.agent != null) return;
        this.agent = GetComponent<NavMeshAgent>();
    }
}