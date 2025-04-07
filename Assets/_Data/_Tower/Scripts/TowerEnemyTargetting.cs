using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// unity automatically add these 2 components
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]

public class TowerEnemyTargetting : Load
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected SphereCollider sphereCollider;
    [SerializeField] protected EnemyControl nearestEnemy;
    [SerializeField] protected List<Enemy> enemyList;

    protected virtual void FixedUpdate()
    {

    }

    protected override void LoadComponent()
    {
        this.LoadRigidbody();
        this.LoadSphereCollider();  
    }

    protected virtual void LoadRigidbody()
    {
        if (this.rb != null) return;
        this.rb = GetComponent<Rigidbody>();
        this.rb.useGravity = false; // turn off gravity of this object otherwise it will fall down
    }

    protected virtual void LoadSphereCollider()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = GetComponent<SphereCollider>();
        this.sphereCollider.radius = 50f;
        this.sphereCollider.isTrigger = true;
    }
}
