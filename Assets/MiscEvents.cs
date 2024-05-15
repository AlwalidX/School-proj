using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents 
{
   public event Action onEnemyKilled;

   public void EnemyKilled()
   {
     if (onEnemyKilled != null)
     {
        onEnemyKilled();
     }
   }
}
