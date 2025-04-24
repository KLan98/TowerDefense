using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class LazerBeam : Load
{
    [SerializeField] protected CapsuleCollider capsuleCollider;
    [SerializeField] protected int damage = 1;

    protected override void LoadComponent()
    {
        this.LoadCapsuleCollider();
    }

    protected virtual void LoadCapsuleCollider()
    {
        if(this.capsuleCollider != null) return;
        this.capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        this.capsuleCollider.height = 500f;
    }

    // This method is called automatically by Unity when another collider enters this object's trigger collider.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has the layer "Enemy"
        // This ensures bullets only react to enemies, ignoring other objects like turrets or terrain.
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Log a message to the console for debugging purposes
            //Debug.Log(other.gameObject.transform.parent.name + " hit");
            DamageReceiver damageReceiver = other.gameObject.transform.parent.GetComponentInChildren<DamageReceiver>();
            if (damageReceiver == null) return;
            this.DamageSend(damageReceiver);
        }
    }

    public void Disable()
    {
        CancelInvoke(); // Ensure no duplicate invoke on reuse
        LazerPool.Instance.ReturnBullet(this);
    }

    protected virtual void DamageSend(DamageReceiver damageReceiver)
    {
        damageReceiver.Deduct(this.damage);
    }
}
