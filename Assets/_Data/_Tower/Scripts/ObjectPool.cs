using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : Load
{
    protected abstract void InitializePool();
    protected abstract void LoadBulletPrefabType();
}
