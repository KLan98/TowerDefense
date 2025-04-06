using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Path : Load
{
    [SerializeField] protected List<Point> points;
    public List<Point> Points => points;

    protected override void LoadComponent()
    {
        this.LoadPoints();
    }

    protected virtual void LoadPoints()
    {
        if (this.points.Count > 0) return;
        foreach (Transform child in transform)
        {
            Point point = child.GetComponent<Point>();
            this.points.Add(point);
        }
    }

    public virtual Point GetPoint(int index)
    {
        return this.points[index];
    }
}
