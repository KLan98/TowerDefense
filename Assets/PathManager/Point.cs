using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Point : Load
{
    [SerializeField] GameObject nextPoint;
    [SerializeField] GameObject currentPoint;
    [SerializeField] List<GameObject> pointsList = new List<GameObject>();

    protected override void LoadComponent()
    {
        this.LoadPointsList();
        this.LoadCurrentAndNextPoints();
    }

    protected virtual void LoadPointsList()
    {
        // copy all siblings into a list
        GameObject path = transform.parent.gameObject;
        foreach (Transform sibling in path.transform)
        {
            pointsList.Add(sibling.gameObject);
        }
    }

    protected virtual void LoadCurrentAndNextPoints()
    {
        foreach (GameObject points in pointsList)
        {
            if (transform.name == points.name)
            {
                this.currentPoint = points;
            }
            this.nextPoint = pointsList[pointsList.IndexOf(currentPoint) + 1];
        }

    }
}
