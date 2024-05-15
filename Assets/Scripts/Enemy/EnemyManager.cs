using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DevionGames.StatSystem;

public class EnemyManager : MonoBehaviour
{

     public NavMeshAgent AiAgent;

      GameObject Player;
        public StatsHandler m_Handler;
   public float Damage = 15f;
    public float RateOfAttack = 0.4f;


 public float attackRate = 2.0f; // Time between attacks in seconds
    private float lastAttackTime = 0.0f;
      

    public Transform[] points;
     Transform nextTarget;
     int currentPointIndex;
     float disToFollow = 7f;

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
        enemyStates = States.Patrol;
        currentPointIndex = 0;
        nextTarget = points[currentPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        float DistToPlayer = Vector3.Distance(transform.position, Player.transform.position);
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

        if (DistToPlayer <= disToFollow)
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
        if(Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.transform.position.x, 0, Player.transform.position.z)) >= 7f)
        {
            enemyStates = States.Patrol;
        }

        if(Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.transform.position.x, 0, Player.transform.position.z)) <= 1f)
        {
            enemyStates = States.Attack;
        }

        
    }
    void Attack()
    {
     if (Time.time - lastAttackTime >= attackRate && enemyStates == States.Attack)
     {
       DamagePlayer();
       lastAttackTime = Time.time; // Update last attack time
     }
     
    }
  
  void DamagePlayer()
  {
    m_Handler.ApplyDamage("Health", Damage);
  }

}
