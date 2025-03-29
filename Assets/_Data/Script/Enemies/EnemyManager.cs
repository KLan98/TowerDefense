using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Wolf wolfComponent; // Store reference to Wolf
    private Ghost ghostComponent;
    private Zombie zombieComponent;

    private Enemy minWeightEnemy;
    private Enemy maxWeightEnemy;  

    List<Enemy> enemies = new List<Enemy>(); // create list named enemies

    private void Awake()
    {
        this.LoadEnemies();
    }

    private void Start()
    {
        this.LoadMinWeightEnemy();
        this.LoadMaxWeightEnemy();  
    }

    //private void FixedUpdate()
    //{
    //    this.TestMethod();
    //}

    //void TestMethod()
    //{
    //    wolfComponent.Moving();
    //    wolfComponent.HP();

    //    ghostComponent.HP();
    //}

    private void LoadMinWeightEnemy()
    {
        this.minWeightEnemy = enemies[0];
        foreach (Enemy enemy in this.enemies)
        {
            if (enemy.GetWeight() < this.minWeightEnemy.GetWeight())
            {
                this.minWeightEnemy = enemy;    
            }
        }
        Debug.Log("Min Weight Enemy" + " " + this.minWeightEnemy);
    }

    private void LoadMaxWeightEnemy()
    {
        this.maxWeightEnemy = enemies[0];
        foreach (Enemy enemy in this.enemies)
        {
            if (enemy.GetWeight() > this.minWeightEnemy.GetWeight())
            {
                this.maxWeightEnemy = enemy;
            }
        }
        Debug.Log("Max Weight Enemy" + " " + this.maxWeightEnemy);
    }

    protected void LoadEnemies()
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

        GameObject zombie = new GameObject("Zombie");
        zombieComponent = zombie.AddComponent<Zombie>();
        zombie.transform.SetParent(enemyManager.transform);

        foreach (Transform child in enemyManager.transform)
        {
            Enemy enemy = child.GetComponent<Enemy>(); // if enemy is the  subclass of Enemy
            if (enemy == null) continue; // if the enemy is not a subclass of enemy, then skip the next lines 
            this.enemies.Add(enemy); // add enemy into enemies list
            Debug.Log(enemy.GetName() + " " + enemy.GetWeight());
        }
    }
}
