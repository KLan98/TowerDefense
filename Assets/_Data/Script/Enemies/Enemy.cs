using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int currentHp = 100;
    int maxHp = 100;
    bool isDead = true;
    bool isBoss = true;
    float weight = 2.5f;

    EnemyHead head1 = new EnemyHead();  
}
