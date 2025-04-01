using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponent();
    }

    protected virtual void Reset()
    {
        this.LoadComponent();
    }

    protected virtual void LoadComponent()
    {

    }
}
