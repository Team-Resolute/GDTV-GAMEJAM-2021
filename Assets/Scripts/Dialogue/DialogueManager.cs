using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Sound;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class DialogueManager : MonoBehaviour
{
    private static List<DialogueSnippet> dialogue = new List<DialogueSnippet>();
    [SerializeField] private DialogueBox playerDialogueBox = default;
    [SerializeField] private DialogueBox sandmanDialogueBox = default;
    [SerializeField] private DialogueBox motherDialogueBox = default;
    [SerializeField] private DialogueBox minorCharacterDialogueBox = default;
    [SerializeField] private Image defaultBackboard = default;
    private static Coroutine currentDialogueSequence = default;
    public static DialogueManager Instance { get; private set; }
    private float dialogueDisplayTime = 1.5f;
    private bool dialogueInProgress = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    public void NewDialogue()
    {
        dialogue.Clear();
    }

    public void AddDialogueSpeech(Speaker speaker, string speech)
    {
        dialogue.Add(new DialogueSnippet(speaker, speech));
    }

    public void StartDialogue()
    {
        if (dialogueInProgress) { return;}
        dialogueInProgress = true;
        if (currentDialogueSequence != null)
        {
            StopCoroutine(currentDialogueSequence);           
        }
        currentDialogueSequence = StartCoroutine(nameof(ShowDialogue));
    }

    private IEnumerator ShowDialogue()
    {
        Time.timeScale = 0.2f;
        for (int i = 0; i < dialogue.Count; i++)
        {
            if (i<dialogue.Count-1) {defaultBackboard.gameObject.SetActive(true);}
            
            DialogueSnippet currentSnippet = dialogue[i];
            DialogueBox dialogueBox = DetermineDialogueBox(currentSnippet);
            dialogueBox.Show(currentSnippet);
            yield return new WaitUntil(() => dialogueBox.isOperationDone());
            yield return new WaitForSecondsRealtime(dialogueDisplayTime);
            
            if (i>=dialogue.Count-1) {defaultBackboard.gameObject.SetActive(false);}
            dialogueBox.Hide();
            yield return new WaitUntil(() => dialogueBox.isOperationDone());
            dialogueBox.Hide();
        }
        Time.timeScale = 1f;
        dialogueInProgress = false;
    }

    private DialogueBox DetermineDialogueBox(DialogueSnippet snippet)
    {
        DialogueBox dialogueBox = null;
        if (snippet.speaker == Speaker.Mother) 
        { 
            dialogueBox = motherDialogueBox;
            SoundManager.PlaySound(SoundManager.Sound.MotherDialogue, transform.position);   
        }
        if (snippet.speaker == Speaker.Sandman) 
        { 
            dialogueBox = sandmanDialogueBox; 
            SoundManager.PlaySound(SoundManager.Sound.SandmanDialogue, transform.position);   
        }

        if (snippet.speaker == Speaker.MinorCharacter)
        {
            dialogueBox = minorCharacterDialogueBox;
            SoundManager.PlaySound(SoundManager.Sound.MinorCharacterDialogue, transform.position);
        }
        if (snippet.speaker == Speaker.Player) { dialogueBox = playerDialogueBox; }

        if (dialogueBox == null)
        {
            Debug.LogError("DialogueBox is null.");
            Debug.Log("Currenty speaker is " + snippet.speaker);
        }
        if (dialogueBox) { return dialogueBox; }
        return null;
    }
}