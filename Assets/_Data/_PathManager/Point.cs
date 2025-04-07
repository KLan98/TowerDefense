using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Point : Load
{
    [SerializeField] protected Point nextPoint;
    public Point NextPoint => nextPoint;

    protected override void LoadComponent()
    {
        this.InitNextPoints();
    }

    protected virtual void InitNextPoints()
    {
        // find parent of this object
        int siblingIndex = transform.GetSiblingIndex();

        if (siblingIndex + 1 < this.transform.parent.childCount)
        {
            Transform nextSibling = transform.parent.GetChild(siblingIndex + 1);
            this.nextPoint = nextSibling.GetComponent<Point>();    
        }
    }
}
