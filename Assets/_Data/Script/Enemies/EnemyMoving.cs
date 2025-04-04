using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// control every aspects of moving an enemy 
public class EnemyMoving : Load
{
    [SerializeField] GameObject target;
    [SerializeField] EnemyControl enemyControl;
    [SerializeField] PathManager pathManager;
    [SerializeField] Path enemyPath;
    [SerializeField] int pathIndex;

    void FixedUpdate()
    {
        this.MoveToTarget();
    }

    protected void Start()
    {
        this.LoadEnemyPath();   
    }

    protected override void LoadComponent()
    {
        this.LoadPathManager();
        this.LoadEnemyControl();
        this.LoadTarget();
    }

    protected virtual void LoadEnemyControl()
    {
        if (enemyControl != null) return;
        enemyControl = transform.parent.GetComponent<EnemyControl>();
    }

    protected virtual void LoadPathManager()
    {
        GameObject pathManagerObject = GameObject.Find("PathManager");
        if (this.pathManager != null) return;
        this.pathManager = pathManagerObject.GetComponent<PathManager>();
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

    protected virtual void LoadEnemyPath()
    {
        if (this.enemyPath != null) return;
        this.enemyPath = pathManager.GetPaths(this.pathIndex);
    }
}
