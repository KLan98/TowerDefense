using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int currentHp = 100;
    int maxHp = 100;
    bool isDead = true;
    bool isBoss = true;
    float weight = 2.5f;
    string enemyName;
    float minWeight = 1f;
    float maxWeight = 10f;

    protected virtual void InitData()
    {
        this.weight = this.GetRandomWeight();
    }

    protected virtual float GetRandomWeight()
    {
        return Random.Range(this.minWeight, this.maxWeight);
    }

    float GetWeight()
    {
        return this.weight;
    }

    public virtual string GetName()
    {
        return this.enemyName;
    }

    public void Moving()
    {
        string message = this.GetName() + " " + "Moving";
        Debug.Log(message);
    }

    public void HP()
    {
        string message = this.GetName() + " " + "-1HP";
        Debug.Log(message);
    }
}
