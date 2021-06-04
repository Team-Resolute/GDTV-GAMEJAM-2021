using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechEventOnApproach : MonoBehaviour
{
    private List<DialogueSnippet> dialogueSnippets = new List<DialogueSnippet>();
    [SerializeField] private List<Speaker> speakers = new List<Speaker>();
    [SerializeField] private List<string> speeches = new List<string>();
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return;}
        if (speakers.Count > speeches.Count) { return;}
        
        if (speakers.Count > 0)
        {
            DialogueManager.Instance.NewDialogue();
            for (int i=0; i<speakers.Count; i++)
            {
                DialogueManager.Instance.AddDialogueSpeech(speakers[i], speeches[i]);
            }
            DialogueManager.Instance.StartDialogue();
        }
        Destroy(this.gameObject);
        
        
        
            
        
    }
}
