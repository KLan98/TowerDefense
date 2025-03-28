using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Wolf wolfComponent; // Store reference to Wolf
    private Ghost ghostComponent;

    List<Enemy> enemies = new List<Enemy>(); // create list named enemies

    private void Awake()
    {
        this.LoadEnemies();
    }

    private void FixedUpdate()
    {
        this.TestMethod();
    }

    void TestMethod()
    {
        wolfComponent.Moving();
        wolfComponent.HP();

        ghostComponent.HP();
    }

    protected virtual void LoadEnemies()
    {
        // Find the parent GameObject by name
        GameObject enemyManager = GameObject.Find("EnemyManager");

        // spawn game object called Wolf
        GameObject wolf = new GameObject("Wolf");
        wolfComponent = wolf.AddComponent<Wolf>();
        wolf.transform.SetParent(enemyManager.transform);

        GameObject ghost = new GameObject("Ghost");
        ghostComponent = ghost.AddComponent<Ghost>();
        ghost.transform.SetParent(enemyManager.transform);

        foreach (Transform child in transform)
        {
            Enemy enemy = child.GetComponent<Enemy>(); // if enemy is the  subclass of Enemy
            if (enemy == null) continue; // if the enemy is not a subclass of enemy, then skip the next lines 
            this.enemies.Add(enemy); // add enemy into enemies list
        }
    }
}
