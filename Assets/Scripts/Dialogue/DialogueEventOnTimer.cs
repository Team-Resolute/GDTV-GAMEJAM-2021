using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventOnTimer : MonoBehaviour
{
    [SerializeField] private float timeUntilStart = 0f; 
    [SerializeField] private List<Speaker> speakers = new List<Speaker>();
    [SerializeField] private List<string> speeches = new List<string>();
    

    private void Start()
    {
        Invoke(nameof(StartDialogue), timeUntilStart);
    }


    private void StartDialogue()
    {
        if (speakers.Count == speeches.Count && speakers.Count > 0)
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
