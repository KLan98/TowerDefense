using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Path : Load
{
    [SerializeField] private List<Point> points = new();
    public ReadOnlyCollection<Point> Points => points.AsReadOnly(); // expose the list as read only 


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
}
