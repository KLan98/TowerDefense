using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

// use for loading navmesh agent 
public class EnemyControl : Load
{
    [SerializeField] protected Transform model;
    [SerializeField] protected NavMeshAgent agent;
    public NavMeshAgent Agent => agent;

    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    protected override void LoadComponent()
    {
        this.LoadNavMeshAgent();
        this.LoadModel();
        this.LoadAnimator();
    }

    protected virtual void LoadNavMeshAgent()
    {
        if (this.agent != null) return;
        this.agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void LoadAnimator()
    {
        if(this.animator != null) return;
        // load animator component from model
        this.animator = this.model.GetComponent<Animator>(); 
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model");
        this.model.localPosition = new Vector3(0f, 0f, 0f);
    }
}