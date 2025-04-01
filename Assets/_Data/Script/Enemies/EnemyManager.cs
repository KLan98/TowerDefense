using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<Enemy> enemies = new List<Enemy>(); // create list named enemies
    public ReadOnlyCollection<Enemy> Enemies => enemies.AsReadOnly(); // expose the list as read only 
}
