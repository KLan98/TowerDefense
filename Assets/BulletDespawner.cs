using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawner : MonoBehaviour
{
    [SerializeField] private float despawnTime = 3f;

    // is called when object becomes enabled and active
    void OnEnable()
    {
        StartCoroutine(DespawnAfterTime());
    }

    private System.Collections.IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(despawnTime);
        // destroy the parent of BulletDespawner
        Destroy(transform.parent.gameObject);
        Debug.Log("Bullet despawned");
    }
}
