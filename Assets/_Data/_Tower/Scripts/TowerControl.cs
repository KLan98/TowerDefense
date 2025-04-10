using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TowerControl : Load
{
    [SerializeField] protected Transform model;
    [SerializeField] protected Transform rotator;
    public Transform Rotator => rotator;

    protected override void LoadComponent()
    {
        this.LoadModel();
        this.LoadRotator();
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = GameObject.Find(Const.TURRET_MODEL).GetComponent<Transform>();    
    }
    protected virtual void LoadRotator()
    {
        if (this.rotator != null) return;
        this.rotator = GameObject.Find(Const.TURRET_ROTATOR).GetComponent<Transform>();
    }
}
