using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class TowerShooting : Load
{
    // enemy control component of the target
    [SerializeField] protected EnemyControl nearestEnemyControl;
    [SerializeField] public float rotationSpeed = 40f;
    [SerializeField] protected float intervalBetweenShots = 0.15f;
    [SerializeField] protected bool isFiring = false;
    [SerializeField] protected bool canFire = false;
    [SerializeField] protected MachineGunControl machineGunControl;
    [SerializeField] protected TowerEnemyTargetting towerEnemyTargetting;

    void Update()
    {
        this.LoadNearestEnemyControl();
        this.CheckFiring();
    }
    protected override void LoadComponent()
    {
        this.LoadMachineGunControl();
        this.LoadTowerEnemyTargetting();
    }

    protected virtual void LoadMachineGunControl()
    {
        if (this.machineGunControl != null) return;
        this.machineGunControl = transform.parent.GetComponent<MachineGunControl>();
    }

    protected virtual void LoadTowerEnemyTargetting()
    {
        if (this.towerEnemyTargetting != null) return;
        this.towerEnemyTargetting = machineGunControl.GetComponentInChildren<TowerEnemyTargetting>();
    }

    protected virtual void LookAtTarget()
    {
        // if no enemy inrange then do nothing
        if (towerEnemyTargetting.InRangeEnemies.Count == 0) return;

        // Calculate the relative distance from the turret to the target
        Vector3 direction = nearestEnemyControl.GetComponentInChildren<EnemyTargetable>().transform.position - machineGunControl.Rotator.transform.position;

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
        machineGunControl.Rotator.rotation = Quaternion.Lerp(
        machineGunControl.Rotator.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }

    protected virtual void FireAtTarget()
    {
        this.nearestEnemyControl = towerEnemyTargetting.NearestEnemy;

        // spawn bullets and fire
        this.machineGunControl.BulletSpawner.StartFiring(BulletPool.Instance.TowerBullet, intervalBetweenShots);
    }

    void OnDrawGizmos()
    {
        if (nearestEnemyControl != null && machineGunControl.Rotator != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                machineGunControl.TowerFirePoint.transform.position,
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
            LookAtTarget();;

            //Debug.Log("Dot product between " + nearestEnemyControl.GetComponentInChildren<EnemyTargetable>().name + " and " + machineGunControl.TowerFirePoint.name + " = " + dot);

            // only fire when enemy within a certain angle threshold before firing 
            if (this.canFire == true)
            {
                FireAtTarget(); // Starts firing bullets (spawns coroutine)
                this.isFiring = true; // Mark that we're now firing so we don't start again
            }

            else if (this.canFire == false)
            {
                machineGunControl.BulletSpawner.StopFiring();
                this.isFiring = false;
            }

            //else if (dot <= aimDotThreshold)
            //{
            //    machineGunControl.BulletSpawner.StopFiring();
            //    this.isFiring = false;
            //}
        }

        else if (machineGunControl.BulletSpawner.firingCoroutine == null)
        {
            machineGunControl.BulletSpawner.StopFiring();
            this.isFiring = false;
            //Debug.Log("Set isFiring = false, reason: Firing coroutine stopped");
        }

        else if (towerEnemyTargetting.InRangeEnemies.Count == 0)
        {
            machineGunControl.BulletSpawner.StopFiring();
            this.isFiring = false;
            //Debug.Log("Set isFiring = false, reason: In range enemies = " + towerEnemyTargetting.InRangeEnemies.Count);
        }

        else if (this.nearestEnemyControl == null)
        {
            machineGunControl.BulletSpawner.StopFiring();
            this.isFiring = false;
            //Debug.Log("Set isFiring = false, reason: Target enemy control = null");
        }
    }
}
