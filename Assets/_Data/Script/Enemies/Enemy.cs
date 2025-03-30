using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //int currentHp = 100;
    //int maxHp = 100;
    //bool isDead = true;
    //bool isBoss = true;
    float weight = 2.5f;
    //string enemyName;
    float rangeMinWeight = 1f;
    float rangeMaxWeight = 10f;

    public abstract string GetName();

    private void OnEnable()
    {
        this.InitData();
    }

    //private void Reset()
    //{
    //    Debug.Log("Reset");
    //    this.InitData();
    //}

    protected virtual void InitData()
    {
        this.weight = this.GetRandomWeight();
    }

    protected virtual float GetRandomWeight()
    {
        return Random.Range(this.rangeMinWeight, this.rangeMaxWeight);
    }

    public float GetWeight()
    {
        return this.weight;
    }

    //public void Moving()
    //{
    //    string message = this.GetName() + " " + "Moving";
    //    Debug.Log(message);
    //}

    //public void HP()
    //{
    //    string message = this.GetName() + " " + "-1HP";
    //    Debug.Log(message);
    //}
}
