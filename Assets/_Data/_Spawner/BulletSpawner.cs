using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//protected TowerFirePoint towerFirePoint;
//// clone the given prefab
//public virtual TowerBullet Spawn(TowerBullet bulletPrefab)
//{
//    TowerBullet newObject = Instantiate(bulletPrefab);
//    return newObject;   
//}

public class BulletSpawner : Load
{
    [SerializeField] protected TowerControl towerControl;

    // This variable will hold the reference to the currently running bullet-spawning coroutine.
    // It's how we check if bullets are already being spawned (to prevent duplicates),
    // and how we stop it cleanly when needed.
    public Coroutine firingCoroutine;

    // Unity calls this when the script is loaded.
    protected override void LoadComponent()
    {
        this.LoadTowerControl();
    }

    // Connects this spawner to the parent TowerControl if it's not already assigned.
    protected virtual void LoadTowerControl()
    {
        if (this.towerControl != null) return;
        this.towerControl = transform.parent.GetComponent<TowerControl>();
    }

    // This coroutine continuously spawns bullets with a delay between each shot.
    private IEnumerator SpawnBullets(TowerBullet bulletPrefab, float interval)
    {
        while (true)
        {
            // Instantiate a bullet at the tower's fire point position and rotation.
            Instantiate(
                bulletPrefab,
                towerControl.TowerFirePoint.transform.position,
                towerControl.TowerFirePoint.transform.rotation
            );

            // Wait before spawning the next bullet.
            // This controls the fire rate (e.g., 0.5s between shots).
            yield return new WaitForSeconds(interval);
        }
    }

    // Starts firing bullets, but only if it's not already firing.
    public void StartFiring(TowerBullet bulletPrefab, float interval)
    {
        // Check if a coroutine is already running
        if (firingCoroutine == null)
        {
            // Start the bullet-spawning coroutine and save its reference
            firingCoroutine = StartCoroutine(SpawnBullets(bulletPrefab, interval));
            //Debug.Log("Start Firing");
        }
        // If it's already firing, do nothing — avoids starting multiple coroutines
    }

    // Stops firing bullets if currently firing
    public void StopFiring()
    {
        // Only stop if a coroutine is running
        if (firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);   // Stop the coroutine
            firingCoroutine = null;          // Clear the reference
            //Debug.Log("Stop Firing");
        }
    }
}
