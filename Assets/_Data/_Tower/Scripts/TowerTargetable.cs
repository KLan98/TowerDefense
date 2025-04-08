using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyTargetable : Load
{
    [SerializeField] protected SphereCollider sphereCollider;

    protected override void LoadComponent()
    {
        this.LoadSphereCollider();
    }

    protected virtual void LoadSphereCollider()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = GetComponent<SphereCollider>();
        this.sphereCollider.radius = 2f;
        this.sphereCollider.isTrigger = false;   
    }
}
