using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBanditQuestStep : QuestStep
{
   private int banditsKilled = 0;
   private int banditsToKill = 5;
  
   private void OnEnable()
   {
    GameEventsManager.instance.miscEvents.onEnemyKilled += BanditsKilled;
   }

   private void OnDisable() 
   {
    GameEventsManager.instance.miscEvents.onEnemyKilled -= BanditsKilled;
   }

   private void BanditsKilled()
   {
    if (banditsKilled < banditsToKill)
    {
        banditsKilled++;
        UpdateState();
    }

    if (banditsKilled >= banditsToKill)
    {
        FinishQuestStep();
    }
   }

   private void UpdateState()
   {
    string state = banditsKilled.ToString();
    string status = "Collected " + banditsKilled + " / " + banditsToKill + " coins.";
    ChangeState(state, status);
   }

    protected override void SetQuestStepState(string state)
    {
        this.banditsKilled = System.Int32.Parse(state);
        UpdateState();
    }
}
