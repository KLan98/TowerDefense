using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// not attached to an object
public abstract class EnemyAbstract : Load
{
    [SerializeField] protected EnemyControl enemyControl;
    [SerializeField] protected EnemyMoving enemyMoving;
    [SerializeField] protected EnemyTargetable enemyTargetable;

    protected override void LoadComponent()
    {
        LoadEnemyControl();
        LoadEnemyMoving();
        LoadEnemyTargetable();  
    }

    protected virtual void LoadEnemyControl()
    {

    }

    protected virtual void LoadEnemyMoving()
    {

    }
    protected virtual void LoadEnemyTargetable()
    {

    }
}
