using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimingPoint : Load
{
    protected override void LoadComponent()
    {
        this.InitPosition();
    }

    protected virtual void InitPosition()
    {
        gameObject.transform.localPosition = new Vector3(0, 5.5f, 0);
    }
}
