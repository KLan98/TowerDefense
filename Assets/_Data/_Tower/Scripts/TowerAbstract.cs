using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerAbstract : Load
{
    [SerializeField] protected TowerControl towerControl;

    protected override void LoadComponent()
    {
        this.LoadTowerControl();
    }

    protected virtual void LoadTowerControl()
    {
        if (this.towerControl != null) return;
        this.towerControl = transform.parent.GetComponent<TowerControl>();
    }
}
