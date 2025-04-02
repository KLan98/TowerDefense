using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LegGrounding : Load
{
    public Transform enemyTransform;

    private void FixedUpdate()
    {
        this.GroundedEnemy();
    }

    protected override void LoadComponent()
    {
        this.LoadEnemyTransform(); 
    }

    protected virtual void LoadEnemyTransform()
    {
        //Debug.Log("Enemy transform: " + this.enemyTransform.position);
        if (this.enemyTransform != null) return;
        this.enemyTransform = transform.GetComponent<Transform>();
    }

    protected virtual void GroundedEnemy()
    {
        RaycastHit hit;
        if(Physics.Raycast(this.enemyTransform.position, Vector3.down, out hit))
        {
            Debug.DrawLine(this.enemyTransform.position, hit.point, Color.red);
            this.enemyTransform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}
