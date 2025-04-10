using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class TowerShooting : TowerAbstract
{
    // enemy control component of the target
    [SerializeField] protected EnemyControl targetEnemyControl;
    [SerializeField] public float rotationSpeed = 1;

    protected virtual void FixedUpdate()
    {
        this.LookAtTarget();
    }

    protected virtual void LookAtTarget()
    {
        if (targetEnemyControl == null) return;

        // Calculate the relative distance from the turret to the target
        Vector3 direction = targetEnemyControl.transform.position - towerControl.Rotator.transform.position;

        // We only want to rotate left/right, so we ignore the Y (vertical) difference.
        // This ensures the turret doesn't tilt up or down — only turns on the horizontal plane.
        direction.y = 0f;
        Debug.Log(direction);

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

    void OnDrawGizmos()
    {
        if (targetEnemyControl != null && towerControl?.Rotator != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                towerControl.Rotator.position,
                targetEnemyControl.GetComponentInChildren<EnemyAimingPoint>().transform.position
            );
        }
    }
}
