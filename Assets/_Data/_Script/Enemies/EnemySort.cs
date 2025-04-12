using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySort : MonoBehaviour
{
    //[SerializeField] EnemyManager enemyManager;

    //List<Enemy> sortedEnemies = new List<Enemy>();

    //private void Start()
    //{
    //    this.sortedEnemies = this.SortWeightIncrease();
    //    this.ShowSortedEnemies();
    //}
    //public List<Enemy> SortWeightIncrease()
    //{
    //    List<Enemy> sortedEnemies = new List<Enemy>(enemyManager.Enemies); // Copy original list so that it stayed unmodified

    //    // get length of sortedEnemies List which also means get length of enemyManager.Enemies
    //    int n = sortedEnemies.Count;

    //    // advance the position through the entire array
    //    for (int i = 0; i < n - 1; i++)
    //    {
    //        // assume the currentMin is the first element
    //        int currentMin = i; 

    //        // test against elements after i to find the smallest 
    //        for (int j = i + 1; j < n; j++)
    //        {
    //            // if this element is less, then it is the new minimum 
    //            if (sortedEnemies[j].GetWeight() < sortedEnemies[currentMin].GetWeight())
    //            {
    //                // found new minimum; remember its index 
    //                currentMin = j;
    //            }
    //        }

    //        // if currentMin has changed, it means a smaller element was found, so swap it with the current position i.
    //        if (currentMin != i)
    //        {
    //            Enemy temp = sortedEnemies[i];
    //            sortedEnemies[i] = sortedEnemies[currentMin];
    //            sortedEnemies[currentMin] = temp;
    //        }
    //    }

    //    return sortedEnemies;
    //}

    //void ShowSortedEnemies()
    //{
    //    foreach(var enemy in this.sortedEnemies)
    //    {
    //        Debug.Log("Enemy weight: " + enemy.GetWeight());
    //    }
    //}
}
