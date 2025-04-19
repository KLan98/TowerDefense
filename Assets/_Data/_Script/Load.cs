using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// purpose is to load component everytime awake
public class Load : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponent();
        //Debug.Log("Play hit");
    }

    protected virtual void Reset()
    {
        this.LoadComponent();
        //Debug.Log("Reset pressed");
    }

    protected virtual void LoadComponent()
    {

    }

    protected virtual void ResetValue()
    {
        // reset some value
    }
}
