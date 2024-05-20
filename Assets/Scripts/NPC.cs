using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DevionGames.StatSystem;

public class NPC : MonoBehaviour
{

     public NavMeshAgent AiAgent;

    public Transform[] points;
     Transform nextTarget;
     int currentPointIndex;

    enum States
    {
        Idle,
        Patrol,
    }

    States enemyStates;

     void Awake()
    {
        AiAgent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyStates = States.Patrol;
        currentPointIndex = 0;
        nextTarget = points[currentPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
     
        switch (enemyStates)
        {
            case States.Idle: 
                                        Debug.Log("Idle");
                                        break;


            case States.Patrol: 
                                        Patrolling();
                                        AiAgent.SetDestination(nextTarget.position);
                                        Debug.Log("Patrol");
                                        break;
        }
    }

    void Patrolling()
    {
       

        if(Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(nextTarget.position.x, 0, nextTarget.position.z)) <= 0.01f)
        {
            Debug.Log($"Target reached: {nextTarget.gameObject.name}");
            currentPointIndex++;
            
            if(currentPointIndex >= points.Length)
            {
                currentPointIndex = 0;
            }

            nextTarget = points[currentPointIndex];
        }

    }

}
