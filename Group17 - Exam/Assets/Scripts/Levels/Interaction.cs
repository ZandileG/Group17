using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public DialogueManager dialogueManager; // Reference to your DialogueManager script.
    public Dialogue dialogue;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // To prevent multiple triggers if the player stays inside the trigger zone.

            // Call the StartDialogue function from the DialogueManager script.
            dialogueManager.StartDialogue(dialogue);
        }
        
           // Time.timeScale = 0;
        
    }
}

