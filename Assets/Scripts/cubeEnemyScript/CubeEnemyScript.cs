using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cubeEnemyScript : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(GameObject.Find("PlayerBody").GetComponent<Transform>().position);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
