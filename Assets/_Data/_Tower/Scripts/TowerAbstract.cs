using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// not attached to game object
public abstract class TowerAbstract : Load
{
    [SerializeField] protected TowerControl towerControl;
    [SerializeField] protected TowerBullet towerBullet;
    [SerializeField] protected TowerEnemyTargetting towerEnemyTargetting;

    protected override void LoadComponent()
    {
        this.LoadTowerControl();
        this.LoadTowerBullet();
        this.LoadTowerEnemyTargetting();
    }

    protected virtual void LoadTowerControl()
    {
        if (this.towerControl != null) return;
        this.towerControl = transform.parent.GetComponent<TowerControl>();
    }

    protected virtual void LoadTowerBullet()
    {
        if (this.towerBullet != null) return;
        this.towerBullet = towerControl.GetComponentInChildren<TowerBullet>();    
    }

    protected virtual void LoadTowerEnemyTargetting()
    {
        if (this.towerEnemyTargetting != null) return;
        this.towerEnemyTargetting = towerControl.GetComponentInChildren<TowerEnemyTargetting>();
    }
}
