using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : Load
{
    [SerializeField] protected List<Path> paths = new List<Path>();

    protected override void LoadComponent()
    {
        this.LoadPaths();    
    }

    protected virtual void LoadPaths()
    {
        if (this.paths.Count > 0) return;
        foreach (Transform child in transform)
        {
            Path path = child.GetComponent<Path>(); 
            this.paths.Add(path);
        }
    }
}
