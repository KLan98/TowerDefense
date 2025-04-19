using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGunControl : TowerControl
{
    protected override void LoadComponent()
    {
        this.LoadModel();
        this.LoadRotator();
        this.LoadTowerFirePoint();
    }

    protected override void LoadModel()
    {
        if (this.model != null) return;
        this.model = GameObject.Find(Const.LAZER_MODEL).GetComponent<Transform>();
    }

    protected override void LoadRotator()
    {
        if (this.rotator != null) return;
        this.rotator = GameObject.Find(Const.LAZER_ROTATOR).GetComponent<Transform>();
    }
}
