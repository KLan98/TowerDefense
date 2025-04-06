using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantMoving : EnemyMoving
{
    protected override void ResetValue()
    {
        this.pathIndex = 0;
    }
}
