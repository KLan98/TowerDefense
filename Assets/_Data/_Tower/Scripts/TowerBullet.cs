using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : Load
{
    private float bulletSpeed = 65;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] protected int damage = 2;

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

    // This method is called automatically by Unity when another collider enters this object's trigger collider.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has the layer "Enemy"
        // This ensures bullets only react to enemies, ignoring other objects like turrets or terrain.
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Log a message to the console for debugging purposes
            Debug.Log(other.gameObject.transform.parent.name + " hit");

            // Return this bullet to the object pool instead of destroying it
            // This improves performance by reusing bullets instead of constantly instantiating/destroying them.
            BulletPool.Instance.ReturnBullet(this);

            DamageReceiver damageReceiver = other.gameObject.transform.parent.GetComponentInChildren<DamageReceiver>();
            if (damageReceiver == null) return;
            this.DamageSend(damageReceiver);
        }
    }

    private void Disable()
    {
        CancelInvoke(); // Ensure no duplicate invoke on reuse
        BulletPool.Instance.ReturnBullet(this);
    }

    protected virtual void DamageSend(DamageReceiver damageReceiver)
    {
        damageReceiver.Deduct(this.damage);
    }
}