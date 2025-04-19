using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSpawner : Load
{
    [SerializeField] protected LazerGunControl lazerGunControl;

    // The current active laser beam
    [SerializeField] protected LazerBeam currentBeam;

    protected override void LoadComponent()
    {
        this.LoadTowerControl();
    }

    protected virtual void LoadTowerControl()
    {
        if (this.lazerGunControl != null) return;
        this.lazerGunControl = transform.parent.GetComponent<LazerGunControl>();
    }

    public void StartFiring(LazerBeam beamPrefab)
    {
        if (currentBeam == null)
        {
            // Get a beam from the pool
            currentBeam = LazerPool.Instance.GetBullet();


            // Align it to fire point
            currentBeam.transform.position = lazerGunControl.TowerFirePoint.transform.position;
            currentBeam.transform.rotation = lazerGunControl.TowerFirePoint.transform.rotation;

            // Activate the beam
            currentBeam.gameObject.SetActive(true);

            //Debug.Log(lazerGunControl.gameObject.name + " Start Beam");
        }

        currentBeam.transform.position = lazerGunControl.TowerFirePoint.transform.position;
        currentBeam.transform.rotation = lazerGunControl.TowerFirePoint.transform.rotation;
    }

    public void StopFiring()
    {
        if (currentBeam != null)
        {
            currentBeam.Disable();  // Return to pool & cancel invoke
            currentBeam = null;
            //Debug.Log(lazerGunControl.gameObject.name + " Stop Beam");
        }
    }
}
