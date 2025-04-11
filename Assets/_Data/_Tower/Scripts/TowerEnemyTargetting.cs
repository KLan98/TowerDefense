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
    public EnemyControl NearestEnemy => nearestEnemy;

    [SerializeField] protected List<EnemyControl> inRangeEnemies; // enemies in the radius of sphere collider
    public List<EnemyControl> InRangeEnemies => inRangeEnemies;

    protected virtual void FixedUpdate()
    {
        this.FindNearestEnemy();
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

    // called when the collider of another object enters trigger
    protected virtual void OnTriggerEnter(Collider other)
    {
        this.AddEnemy(other);
    }

    // called when the collider of another object enters trigger
    protected virtual void OnTriggerExit(Collider other)
    {
        this.RemoveEnemy(other);
    }

    // add enemy collider to inRangeEnemies
    protected virtual void AddEnemy(Collider collider)
    {
        // if collider name is different from TowerTargetable do nothing
        if (collider.name != Const.ENEMY_TARGETABLE) return;

        // if not then add colliders parent EnemyControl to list
        this.inRangeEnemies.Add(collider.transform.parent.GetComponent<EnemyControl>());
    }

    protected virtual void RemoveEnemy(Collider collider)
    {
        // if collider name is different from TowerTargetable do nothing
        if (collider.name != Const.ENEMY_TARGETABLE) return;

        // if not then remove colliders parent EnemyControl to list
        this.inRangeEnemies.Remove(collider.transform.parent.GetComponent<EnemyControl>());
    }

    //protected virtual void LoadEnemyManager()
    //{
    //    GameObject enemyManagerObject = GameObject.Find("EnemyManager");
    //    if (this.enemyManager != null) return;
    //    this.enemyManager = enemyManagerObject.GetComponent<EnemyManager>();
    //}

    public virtual void FindNearestEnemy()
    {
        // local variable shouldn't use as [SerializeField] & global variable
        float distanceToEnemy;
        float nearestDistance = Mathf.Infinity;

        // calculate distance to each in range enemy
        foreach (EnemyControl enemy in this.inRangeEnemies)
        {
            distanceToEnemy = Vector3.Distance(gameObject.transform.position, enemy.transform.position);

            if (distanceToEnemy < nearestDistance)
            {
                this.nearestEnemy = enemy;
                nearestDistance = distanceToEnemy;
            }
        }
    }
}
