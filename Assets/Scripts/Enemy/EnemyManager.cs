using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

     public NavMeshAgent AiAgent;

      GameObject Player;

      

    public Transform[] points;
     Transform nextTarget;
     int currentPointIndex;

    enum States
    {
        Idle,
        Patrol,
        FollowPlayer,
        Attack
    }

    States enemyStates;

     void Awake()
    {
        AiAgent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        if(Player != null)
        {
            Debug.Log("Found PLAYER!");
        }
        enemyStates = States.Idle;
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

            case States.FollowPlayer: FollowPlayer();
                                      AiAgent.SetDestination(Player.transform.position);
                                      break;
            case States.Attack: 
                                        Attack();
                                        Debug.Log("Attack");
                                        break;
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            enemyStates = States.Idle;
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            enemyStates = States.Patrol;
            
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            enemyStates = States.Attack;
            
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            enemyStates = States.FollowPlayer;
        }

        //Detect player, Follow Player and then Attack player

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

    void FollowPlayer()
    {
        if(Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.transform.position.x, 0, Player.transform.position.z)) <= 4f)
        {
            enemyStates = States.Patrol;
        }
        
    }
    void Attack()
    {

    }
}
