using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunControl : TowerControl
{
    [SerializeField] protected BulletSpawner bulletSpawner;
    public BulletSpawner BulletSpawner => bulletSpawner;

    protected override void LoadComponent()
    {
        this.LoadModel();
        this.LoadRotator();
        this.LoadBulletSpawner();
        this.LoadTowerFirePoint();
    }

    protected virtual void LoadBulletSpawner()
    {
        if (this.bulletSpawner != null) return;
        this.bulletSpawner = GameObject.Find(Const.BULLET_SPAWNER).GetComponent<BulletSpawner>();
    }

    protected override void LoadModel()
    {
        if (this.model != null) return;
        this.model = GameObject.Find(Const.TURRET_MODEL).GetComponent<Transform>();
    }

    protected override void LoadRotator()
    {
        if (this.rotator != null) return;
        this.rotator = GameObject.Find(Const.TURRET_ROTATOR).GetComponent<Transform>();
    }
}
