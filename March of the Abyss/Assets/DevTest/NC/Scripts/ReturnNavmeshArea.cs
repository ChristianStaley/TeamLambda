using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReturnNavmeshArea : MonoBehaviour
{
    public NavMeshAgent agent;

    public NavMeshHit navMeshHit;


    void PrintPosition()
    {
        if (NavMesh.SamplePosition(agent.transform.position, out navMeshHit, 0.1f, 0))
        {
            Debug.Log(navMeshHit.mask);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PrintPosition();
    }
}