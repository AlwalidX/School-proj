
using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
  public static GameEventsManager instance { get; private set; }
  
  public MiscEvents miscEvents;
  public EnemyEvents enemyEvents;
  public QuestEvents questEvents;
      private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

       miscEvents = new MiscEvents();
       enemyEvents = new EnemyEvents();
       questEvents = new QuestEvents();
    }

}
