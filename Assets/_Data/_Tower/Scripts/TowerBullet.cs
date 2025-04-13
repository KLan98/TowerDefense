using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    private float bulletSpeed = 3f;
    [SerializeField] private float lifetime = 10f;

    private void OnEnable()
    {
        // Automatically disable bullet after a delay
        Invoke(nameof(Disable), lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Example: handle collision with enemies here
        // You could also apply damage here

        Debug.Log("Collider hit");
    }

    private void Disable()
    {
        CancelInvoke(); // Ensure no duplicate invoke on reuse
        BulletPool.Instance.ReturnBullet(this);
    }
}
