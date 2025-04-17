using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class EnemyTargetable : Load
{
    [SerializeField] protected CapsuleCollider capsuleCollider;

    protected override void LoadComponent()
    {
        this.LoadSphereCollider();
    }

    protected virtual void LoadSphereCollider()
    {
        if (this.capsuleCollider != null) return;
        this.capsuleCollider = GetComponent<CapsuleCollider>();
        this.capsuleCollider.radius = 2f;
        this.capsuleCollider.height = 9f;
        this.capsuleCollider.isTrigger = true;   
    }
}
