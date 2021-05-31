using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Level firstLevel = Level.Illusia;
    [SerializeField] private LevelChanger levelChanger = default;
    [SerializeField] private GameObject creditsScreen = default;
    [SerializeField] private DialogueManager dialogueManager = default;
    [SerializeField] Button startButton = default;

    public void ShowCredits()
    {
        creditsScreen.SetActive(true);
    }

    public void HideCredits()
    {
        creditsScreen.SetActive(false);
    }

    public void StartGameFalse()
    {
        startButton.interactable = false;
        dialogueManager.NewDialogue();
        dialogueManager.AddDialogueSpeech(Speaker.Sandman, "Knock, knock.");
        dialogueManager.AddDialogueSpeech(Speaker.Player, "Umm... who's there?");
        dialogueManager.StartDialogue();
    }

    public void StartGame()
    {
        levelChanger.ChangeScene(firstLevel);
    }
    
    
}
