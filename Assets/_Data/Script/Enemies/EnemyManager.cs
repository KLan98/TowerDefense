using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class EnemyManager : Load
{
    [SerializeField] protected List<EnemyControl> enemies;
    public List<EnemyControl> Enemies => enemies;

    protected override void LoadComponent()
    {
        //this.LoadEnemyList();
    }

    protected virtual void LoadEnemyList()
    {
        GameObject enemyManager = GameObject.Find(Const.ENEMY_MANAGER);

        foreach (Transform child in enemyManager.transform)
        {
            EnemyControl enemy = child.GetComponent<EnemyControl>();
            this.enemies.Add(enemy);
        }
    }

}
