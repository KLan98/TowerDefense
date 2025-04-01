using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] GameObject target;
    public NavMeshAgent agent;

    void FixedUpdate()
    {
        agent.SetDestination(target.transform.position);
    }
}
