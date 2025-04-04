using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Point : Load
{
    [SerializeField] Transform nextPoint;
    [SerializeField] Transform parentTransform;

    protected override void LoadComponent()
    {
        this.LoadNextPoint();
    }

    protected virtual void LoadNextPoint()
    {
        // find parent of this 
        this.parentTransform = gameObject.transform.parent;
        int siblingIndex = gameObject.transform.GetSiblingIndex();

        if (siblingIndex + 1 < this.parentTransform.childCount)
        {
             this.nextPoint = this.parentTransform.GetChild(siblingIndex + 1);
        }

    }
}
