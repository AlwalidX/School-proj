using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
   [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

   [SerializeField] private bool playerInRange;

    private void Awake() 
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update() 
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying) 
        {
            visualCue.SetActive(true);
            if (Input.GetKey(KeyCode.I)) 
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator);
            }
        }
        else 
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
         
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
