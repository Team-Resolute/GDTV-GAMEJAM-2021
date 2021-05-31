using System.Collections;
using System.Collections.Generic;
using Sound;
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
        SoundManager.PlaySound(SoundManager.Sound.ClickButton);
        creditsScreen.SetActive(true);
    }

    public void HideCredits()
    {
        SoundManager.PlaySound(SoundManager.Sound.ClickButton);
        creditsScreen.SetActive(false);
    }

    public void StartGameFalse()
    {
        SoundManager.PlaySound(SoundManager.Sound.ClickButton);
        // TODO Check if time should be waited for before starting the false start
        SoundManager.PlaySound(SoundManager.Sound.ButtonFail);
        startButton.interactable = false;
        dialogueManager.NewDialogue();
        dialogueManager.AddDialogueSpeech(Speaker.Sandman, "Knock, knock.");
        dialogueManager.AddDialogueSpeech(Speaker.Player, "Umm... who's there?");
        dialogueManager.StartDialogue();
    }

    public void StartGame()
    {
        SoundManager.PlaySound(SoundManager.Sound.ClickButton);
        levelChanger.ChangeScene(firstLevel);
    }
    
    
}
