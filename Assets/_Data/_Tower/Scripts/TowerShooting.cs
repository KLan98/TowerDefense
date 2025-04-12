using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class TowerShooting : TowerAbstract
{
    // enemy control component of the target
    [SerializeField] protected EnemyControl nearestEnemyControl;
    [SerializeField] public float rotationSpeed = 3f;
    [SerializeField] public float fireRate = 0.3f;
    [SerializeField] protected bool isFiring = false;
    [SerializeField] protected bool canFire = false;

    void Update()
    {
        this.LoadNearestEnemyControl();
        this.CheckFiring();
    }

    protected virtual void LookAtTarget()
    {
        // if no enemy inrange then do nothing
        if (towerEnemyTargetting.InRangeEnemies.Count == 0) return;

        // Calculate the relative distance from the turret to the target
        Vector3 direction = nearestEnemyControl.GetComponentInChildren<EnemyAimingPoint>().transform.position - towerControl.Rotator.transform.position;

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
        towerControl.Rotator.rotation = Quaternion.Lerp(
        towerControl.Rotator.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }

    protected virtual void FireAtTarget()
    {
        this.nearestEnemyControl = towerEnemyTargetting.NearestEnemy;

        // spawn bullets and fire
        this.towerControl.BulletSpawner.StartFiring(towerBullet, fireRate);
    }

    void OnDrawGizmos()
    {
        if (nearestEnemyControl != null && towerControl?.Rotator != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                towerControl.Rotator.position,
                nearestEnemyControl.GetComponentInChildren<EnemyAimingPoint>().transform.position
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

            if (this.canFire == true)
            {
                FireAtTarget(); // Starts firing bullets (spawns coroutine)
                this.isFiring = true; // Mark that we're now firing so we don't start again
            }

            else if (this.canFire == false)
            {
                towerControl.BulletSpawner.StopFiring();
                this.isFiring = false;
            }
        }

        else if (towerControl.BulletSpawner.firingCoroutine == null)
        {
            towerControl.BulletSpawner.StopFiring();
            this.isFiring = false;
            //Debug.Log("Set isFiring = false, reason: Firing coroutine stopped");
        }

        else if (towerEnemyTargetting.InRangeEnemies.Count == 0)
        {
            towerControl.BulletSpawner.StopFiring();
            this.isFiring = false;
            //Debug.Log("Set isFiring = false, reason: In range enemies = " + towerEnemyTargetting.InRangeEnemies.Count);
        }

        else if (this.nearestEnemyControl == null)
        {
            towerControl.BulletSpawner.StopFiring();
            this.isFiring = false;
            //Debug.Log("Set isFiring = false, reason: Target enemy control = null");
        }
    }
}
