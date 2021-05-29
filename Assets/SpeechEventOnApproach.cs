using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechEventOnApproach : MonoBehaviour
{
    [SerializeField] private List<DialogueSnippet> dialogueSnippets = new List<DialogueSnippet>();
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collison.");
        if (!other.CompareTag("Player")) { return;}
        Debug.Log("Collison with player.");
        if (dialogueSnippets.Count > 0)
        {
            DialogueManager.Instance.NewDialogue();
            foreach (DialogueSnippet snippet in dialogueSnippets)
            {
                DialogueManager.Instance.AddDialogueSpeech(snippet.speaker, snippet.speech);
            }
            DialogueManager.Instance.StartDialogue();
            Destroy(this.gameObject);
        }
        
        
        
            
        
    }
}
