using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions 
{
    public void Bind(Story story)
    {
     story.BindExternalFunction("addQuest", (string questName)=> {
          Debug.Log(questName);
          });
    }

    public void Unbind(Story story)
    {
     story.UnbindExternalFunction("addQuest");
    }

}
