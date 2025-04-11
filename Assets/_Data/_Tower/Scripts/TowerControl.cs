using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TowerControl : Load
{
    [SerializeField] protected Transform model;
    [SerializeField] protected Transform rotator;
    public Transform Rotator => rotator;
    [SerializeField] protected TowerBullet towerBullet;
    public TowerBullet TowerBullet => towerBullet;
    [SerializeField] protected BulletSpawner bulletSpawner;
    public BulletSpawner BulletSpawner => bulletSpawner;
    [SerializeField] protected TowerFirePoint towerFirePoint;
    public TowerFirePoint TowerFirePoint => towerFirePoint;

    protected override void LoadComponent()
    {
        this.LoadModel();
        this.LoadRotator();
        this.LoadBulletSpawner();
        this.LoadTowerBullet();
        this.LoadTowerFirePoint();
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

    protected virtual void LoadBulletSpawner()
    {
        if(this.bulletSpawner != null) return;
        this.bulletSpawner = GameObject.Find(Const.BULLET_SPAWNER).GetComponent<BulletSpawner>();   
    }

    protected virtual void LoadTowerBullet()
    {
        if (this.towerBullet != null) return;
        this.towerBullet = gameObject.GetComponentInChildren<TowerBullet>();
    }

    protected virtual void LoadTowerFirePoint()
    {
        if (this.towerFirePoint != null) return;
        this.towerFirePoint = gameObject.GetComponentInChildren<TowerFirePoint>();
    }
}
