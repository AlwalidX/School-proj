using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;

    public Transform enemy;

    public LayerMask whatIsGround, whatIsEnemy;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;


    //States
    public float sightRange, attackRange;
    public bool enemyInSightRange, enemyInAttackRange;

    private void Awake()
    {
        enemy = GameObject.Find("Enemy").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsEnemy);
        enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);

        if (!enemyInSightRange && !enemyInAttackRange) Patroling();
        if (enemyInSightRange && !enemyInAttackRange) ChaseEnemy();
        if (enemyInAttackRange && enemyInSightRange) AttackEnemy();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChaseEnemy()
    {
        agent.SetDestination(enemy.position);
    }

    private void AttackEnemy()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(enemy);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Debug.Log("enemy Attacked");
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        { 
          //   GameEventsManager.instance.miscEvents.EnemyKilled();
             Debug.Log("guard killed");
             Invoke(nameof(DestroyGuard), 0.5f);
        }
    }
    private void DestroyGuard()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}
