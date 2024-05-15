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

  void Update()
 {
    if(!isHostile)
    {
   AiAgent.SetDestination(currentWayPoint.position);
    return;
    }
    else
    {
       float DistToPlayer = Vector3.Distance(transform.position, Player.transform.position);
      if(DistToPlayer < DistToPlayerFollow)
      {
       AiAgent.speed = 0;

      }
      else
      {
         AiAgent.speed = speed;
      }

      if(DistToPlayer < AttackDistance && CanAttack)
      {
        CanAttack = false;
        DamagePlayer();
      }
      AiAgent.SetDestination(Player.transform.position);
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
  }
}