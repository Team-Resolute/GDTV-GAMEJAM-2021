using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Sound;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject buttonHider = default;
    [SerializeField] private float revealDelay = 12f;
    void Start()
    {
        Time.timeScale = 1f;
        Invoke(nameof(Reveal), revealDelay);
        buttonHider.gameObject.SetActive(false);
    }

    public void Reveal()
    {
        buttonHider.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        LevelChanger.Instance.ChangeScene(Level.StartMenu);
        SoundManager.PlaySound(SoundManager.Sound.ClickButton);
    }

    public void QuitGame()
    {
        DialogueManager.Instance.NewDialogue();
        DialogueManager.Instance.AddDialogueSpeech(Speaker.MinorCharacter,"Thank you for playing!");
        SoundManager.PlaySound(SoundManager.Sound.ClickButton);
        Invoke(nameof(Quit), 2f);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
