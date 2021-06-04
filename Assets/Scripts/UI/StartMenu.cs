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
    [SerializeField] private GameObject sleepometer;
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
        dialogueManager.AddDialogueSpeech(Speaker.Player, "Hey, the game didn't start.");
        dialogueManager.AddDialogueSpeech(Speaker.Sandman, "Are you so sure about that?");
        dialogueManager.StartDialogue();
        Invoke(nameof(ActivateSleepometer), 3f);
    }

    public void ActivateSleepometer()
    {
        GameObject sleepometerInstance = Instantiate(sleepometer, new Vector3(0,0,0), Quaternion.identity);
        Sleepometer sleepometerScript = sleepometerInstance.GetComponentInChildren<Sleepometer>();
        sleepometerScript.maxTimer = 20;
        sleepometerScript.StartTimer();    
    }

    public void StartGame()
    {
        //SoundManager.PlaySound(SoundManager.Sound.ClickButton);
        levelChanger.ChangeScene(firstLevel);
    }
    
    
}
