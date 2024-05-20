using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DevionGames.StatSystem;

public class GuardManager : MonoBehaviour
{

    public float health = 35;

     public NavMeshAgent AiAgent;

      GameObject Player;
   public float Damage = 15f;
    public float RateOfAttack = 0.4f;
     EnemyManager m_EnemyManager;

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
        Player = GameObject.FindWithTag("Enemy");
        if(Player != null)
        {
            Debug.Log("Found ENEMY!");
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
       DamageEnemy();
       lastAttackTime = Time.time; // Update last attack time
     }
     
    }
  
  public void DamageGuard(float damage)
  {
    health -= damage;
    if (health <= 0)
    {
        Destroy(gameObject);
        
    }
  }
  void DamageEnemy()
  {
   m_EnemyManager.DamageEnemy(Damage);
  }

}
