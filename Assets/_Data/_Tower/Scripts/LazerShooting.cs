using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerShooting : Load
{
    // enemy control component of the target
    [SerializeField] protected EnemyControl nearestEnemyControl;
    [SerializeField] public float rotationSpeed = 40f;
    //[SerializeField] protected float intervalBetweenShots = 0.15f;
    [SerializeField] protected bool isFiring = false;
    [SerializeField] protected bool canFire = false;
    protected float aimDotThreshold = 0.83f;
    [SerializeField] public float dot;
    [SerializeField] protected LazerGunControl lazerGunControl;
    [SerializeField] protected TowerEnemyTargetting towerEnemyTargetting;
    [SerializeField] protected LazerSpawner lazerSpawner;

    void Update()
    {
        this.LoadNearestEnemyControl();
        this.CheckFiring();
    }

    protected override void LoadComponent()
    {
        this.LoadLazerGunControl();
        this.LoadTowerEnemyTargetting();
        this.LoadLazerSpawner();
    }

    protected virtual void LoadLazerSpawner()
    {
        if (this.lazerSpawner != null) return;
        this.lazerSpawner = lazerGunControl.GetComponentInChildren<LazerSpawner>();
    }

    protected virtual void FireAtTarget()
    {
        this.nearestEnemyControl = towerEnemyTargetting.NearestEnemy;

        // spawn lazer and fire
        this.lazerSpawner.StartFiring(LazerPool.Instance.LazerBeam);
    }
    protected virtual void LoadLazerGunControl()
    {
        if (this.lazerGunControl != null) return;
        this.lazerGunControl = transform.parent.GetComponent<LazerGunControl>();
    }

    protected virtual void LoadTowerEnemyTargetting()
    {
        if (this.towerEnemyTargetting != null) return;
        this.towerEnemyTargetting = lazerGunControl.GetComponentInChildren<TowerEnemyTargetting>();
    }

    void OnDrawGizmos()
    {
        if (nearestEnemyControl != null && lazerGunControl.Rotator != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                lazerGunControl.TowerFirePoint.transform.position,
                nearestEnemyControl.GetComponentInChildren<EnemyTargetable>().transform.position
            );
        }
    }
    protected virtual void LoadNearestEnemyControl()
    {
        if (towerEnemyTargetting.InRangeEnemies.Count > 0)
        {
            this.nearestEnemyControl = towerEnemyTargetting.NearestEnemy;
        }

        else
        {
            this.nearestEnemyControl = null;
            //Debug.Log(this.nearestEnemyControl);
        }
    }

    protected virtual void CheckFiring()
    {
        // Check if we have any enemies in range AND we've already selected a target
        if (towerEnemyTargetting.InRangeEnemies.Count > 0 && this.nearestEnemyControl != null)
        {
            // Rotate the tower to face the current target
            LookAtTarget();

            // calculate absolute dot product of enemy targetable point z vector with tower fire point z vector
            //dot = Mathf.Abs(Vector3.Dot(nearestEnemyControl.GetComponentInChildren<EnemyTargetable>().transform.forward, towerControl.TowerFirePoint.transform.forward));
            dot = Vector3.Dot(nearestEnemyControl.GetComponentInChildren<EnemyTargetable>().transform.forward, lazerGunControl.TowerFirePoint.transform.forward);

            //Debug.Log("Dot product between " + nearestEnemyControl.GetComponentInChildren<EnemyTargetable>().name + " and " + towerControl.TowerFirePoint.name + " = " + dot);

            // only fire when enemy within a certain angle threshold before firing 
            if (this.canFire == true)
            {
                FireAtTarget(); // Starts firing bullets (spawns coroutine)
                this.isFiring = true; // Mark that we're now firing so we don't start again
            }

            else if (this.canFire == false)
            {
                this.lazerSpawner.StopFiring();
                this.isFiring = false;
                //Debug.Log(gameObject.transform.parent.name + " set canFire = false");
            }
        }

        else if (this.towerEnemyTargetting.InRangeEnemies.Count == 0)
        {
            this.lazerSpawner.StopFiring();
            this.isFiring = false;
            //Debug.Log(gameObject.transform.parent.name + " set isFiring = false, reason: In range enemies = " + towerEnemyTargetting.InRangeEnemies.Count);
        }

        else if (this.nearestEnemyControl == null && this.isFiring == true)
        {
            this.lazerSpawner.StopFiring();
            this.isFiring = false;
            //Debug.Log(gameObject.transform.parent.name + " set isFiring = false, reason: Target enemy control = null");
        }
    }

    protected virtual void LookAtTarget()
    {
        // if no enemy inrange then do nothing
        if (towerEnemyTargetting.InRangeEnemies.Count == 0) return;

        // Calculate the relative distance from the turret to the target
        Vector3 direction = nearestEnemyControl.GetComponentInChildren<EnemyTargetable>().transform.position - lazerGunControl.Rotator.transform.position;

        // We only want to rotate left/right, so we ignore the Y (vertical) difference.
        // This ensures the turret doesn't tilt up or down — only turns on the horizontal plane.
        direction.y = 0f;
        //Debug.Log(direction);

        // If the direction becomes zero (target is directly above or same position), skip to avoid errors
        if (direction == Vector3.zero) return; // Prevent NaNs

        // Calculate the target rotation:
        // Quaternion.LookRotation creates a rotation looking in the given direction,
        // assuming the object's forward (Z+ axis) is the "barrel" of the turret.
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly interpolate the turret's rotation toward the target rotation
        // This makes the movement gradual instead of snapping instantly.
        // lerp(A, B, alpha) = A + alpha * (B - A). 
        // Lerp(current rotation, angle between current rotation and object, time)
        lazerGunControl.Rotator.rotation = Quaternion.Lerp(
        lazerGunControl.Rotator.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }
}

