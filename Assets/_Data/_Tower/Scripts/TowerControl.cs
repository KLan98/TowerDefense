using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TowerControl : Load
{
    [SerializeField] protected Transform model;
    protected override void LoadComponent()
    {
        this.LoadModel();
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = GameObject.Find("TurretModel").GetComponent<Transform>();    
    }
}
