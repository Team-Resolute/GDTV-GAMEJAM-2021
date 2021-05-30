using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class DialogueManager : MonoBehaviour
{
    private static List<DialogueSnippet> dialogue = new List<DialogueSnippet>();
    [SerializeField] private DialogueBox playerDialogueBox = default;
    [SerializeField] private DialogueBox sandmanDialogueBox = default;
    [SerializeField] private DialogueBox motherDialogueBox = default;
    [SerializeField] private Image defaultBackboard = default;
    private static Coroutine currentDialogueSequence = default;
    public static DialogueManager Instance { get; private set; }
    private float dialogueDisplayTime = 1.5f;
    
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
        Debug.Log("Dialogue is started.");
        currentDialogueSequence = StartCoroutine(nameof(ShowDialogue));
    }

    private IEnumerator ShowDialogue()
    {
        for (int i = 0; i < dialogue.Count; i++)
        {
            Time.timeScale = 0f;
            DialogueSnippet currentSnippet = dialogue[i];
            DialogueBox dialogueBox = DetermineDialogueBox(currentSnippet);
            dialogueBox.Show(currentSnippet);
            yield return new WaitUntil(() => dialogueBox.isOperationDone());
            yield return new WaitForSecondsRealtime(dialogueDisplayTime);
            Time.timeScale = 1f;
            if (i<dialogue.Count-1) {defaultBackboard.gameObject.SetActive(true);}
            else {defaultBackboard.gameObject.SetActive(false);}
            dialogueBox.Hide();
            yield return new WaitUntil(() => dialogueBox.isOperationDone());
            Debug.Log("Dialogue is finished.");
        }
    }

    private DialogueBox DetermineDialogueBox(DialogueSnippet snippet)
    {
        DialogueBox dialogueBox = null;
        if (snippet.speaker == Speaker.Mother) { dialogueBox = motherDialogueBox; }
        if (snippet.speaker == Speaker.Sandman) { dialogueBox = sandmanDialogueBox; }
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