using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// control every aspects of moving an enemy 
public class EnemyMoving : Load
{
    [SerializeField] protected EnemyControl enemyControl;
    [SerializeField] protected PathManager pathManager;
    [SerializeField] protected Path enemyPath;
    [SerializeField] protected int pathIndex = 1; // default index = 1
    [SerializeField] protected Point currentPoint;
    [SerializeField] protected Point finalPoint;
    [SerializeField] protected float pointDistance = Mathf.Infinity; // distance between two points assign as max value (infinity)
    [SerializeField] protected float stopDistance = 1f; // when this distance reached find next point
    [SerializeField] protected bool isFinalPoint = false; // check if final point is reached
    [SerializeField] protected bool isMoving = false;
    [SerializeField] protected bool canMove = false;

    void FixedUpdate()
    {
        this.Moving();
        this.CheckMoving();
    }

    protected void Start()
    {
        this.LoadEnemyPath(); // always called before FinalPoint
        this.LoadFinalPoint(); // pick a final point based on the chosen path
    }

    // ONLY USED FOR LOADING COMPONENT!!! NOT INIT
    protected override void LoadComponent()
    {
        this.LoadComponentPathManager();
        this.LoadComponentEnemyControl();
    }

    protected virtual void LoadComponentEnemyControl()
    {
        if (enemyControl != null) return;
        enemyControl = transform.parent.GetComponent<EnemyControl>();
    }

    protected virtual void LoadComponentPathManager()
    {
        GameObject pathManagerObject = GameObject.Find("PathManager");
        if (this.pathManager != null) return;
        this.pathManager = pathManagerObject.GetComponent<PathManager>();
    }

    protected virtual void Moving()
    {
        // if agent cannot move then agent isStopped
        if (!this.canMove)
        {
            this.enemyControl.Agent.isStopped = true;
            return;
        }

        this.FindNextPoint();

        this.enemyControl.Agent.SetDestination(this.currentPoint.transform.position);
        Debug.Log(this.transform.parent.name + " " + "is moving to" + " " + this.currentPoint.transform.name + " " + "of" + " " + this.currentPoint.transform.parent.name);
    }

    protected virtual void FindNextPoint()
    {
        this.enemyControl.Agent.isStopped = false;

        // init point, if there is no current point then current point = point_0
        if (this.currentPoint == null)
        {
            this.currentPoint = this.enemyPath.GetPoint(0);
        }

        // calculate Euclidean distance between enemy position and currentPoint
        this.pointDistance = Vector3.Distance(transform.position, this.currentPoint.transform.position);

        // if the Euclidean distance < stop distance then point_x has been reached AND only assign next point if current point different from final point
        if (this.pointDistance < this.stopDistance && this.currentPoint != this.finalPoint)
        {
            Debug.Log(this.currentPoint.transform.name + " " + "reached");
            this.currentPoint = this.currentPoint.NextPoint;
        }

        // final point reached
        else if (this.pointDistance < this.stopDistance && this.currentPoint == this.finalPoint)
        {
            Debug.Log("Final point" + " " + this.currentPoint.transform.name + " " + "reached");
            this.isFinalPoint = true;
            this.canMove = false;
        }
    }

    // not a component
    protected virtual void LoadFinalPoint()
    {
        if (this.finalPoint != null) return;
        // assign final point as the last element of points list
        this.finalPoint = this.enemyPath.GetPoint(enemyPath.Points.Count - 1);
    }

    // depends on pathIndex to pick out a path from pathManager
    protected virtual void LoadEnemyPath()
    {
        if (this.enemyPath != null) return;
        this.enemyPath = pathManager.GetPaths(this.pathIndex);
    }

    // use navmesh to check if the agent is moving
    protected virtual void CheckMoving()
    {
        if (this.enemyControl.Agent.velocity.magnitude > 0)
        {
            this.isMoving = true;
        }

        else
        {
            this.isMoving = false;
        }

        // set parameter to value of isMoving in animator
        this.enemyControl.Animator.SetBool("isMoving", this.isMoving);
    }
}
