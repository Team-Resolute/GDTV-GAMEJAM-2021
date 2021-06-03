using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private Image buttonHider = default;
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
    }

    public void QuitGame()
    {
        DialogueManager.Instance.NewDialogue();
        DialogueManager.Instance.AddDialogueSpeech(Speaker.MinorCharacter,"Thank you for playing!");
        Invoke(nameof(Quit), 2f);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
