using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DevionGames.StatSystem;

public class EnemyController : MonoBehaviour
{
    public bool isHostile;
    public Transform currentWayPoint;
    [Space (12)]
    public NavMeshAgent AiAgent;
                                 GameObject Player;
   
   [Space(12)]

   public float DistToPlayerFollow;
   public float speed = 3f;
   public StatsHandler m_Handler;
   public float Damage = 15f;
   bool CanAttack;
   public float AttackDistance;
   public float RateOfAttack = 0.4f;

void Start()
{
   Player = GameObject.FindWithTag("Player");

   CanAttack = true;
}

    void FixedUpdate()
    {
        if(!isHostile)
        {
            AiAgent.SetDestination(currentWayPoint.position);
            Debug.Log("Enemy not hostile");
        } else
        {
            // Hes hostile
            float DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            if(DistanceToPlayer < DistToPlayerFollow) 
            {
                //WE should follow our player
                AiAgent.speed = speed;
                    AiAgent.SetDestination(Player.transform.position + new Vector3(0,2,0));
                Debug.Log("Following player");
            } else
            {
                //Player out of follow range
                AiAgent.speed = speed;
                Debug.Log("Player out of range");

                //We should probably return him into patrol mode
                isHostile = false;
            }

            if(DistanceToPlayer < AttackDistance && CanAttack)
            {
                Debug.Log("Attacking?");
                CanAttack = false;
                DamagePlayer();
            }
        }
    }

    void DamagePlayer()
  {
    m_Handler.ApplyDamage("Health", Damage);
    Invoke("ResetFire", RateOfAttack);
  }
  
  void ResetFire()
  {
    CanAttack = true;
  }
  
  private void OnDrawGizmosSelected()
  {
   Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, DistToPlayerFollow);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);
  }
}