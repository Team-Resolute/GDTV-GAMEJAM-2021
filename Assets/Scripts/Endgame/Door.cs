using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Monster monster;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract(GameObject player)
    {
        GameObject sleepometer = GameObject.FindGameObjectWithTag("Sleepometer");
        if (sleepometer) {sleepometer.SetActive(false);}
        SoundManager.PlaySound(SoundManager.Sound.DoorUsed);
        Invoke("PlayDialogue",0.25f);
        Invoke("Win",6f);
    }

    private void PlayDialogue()
    {
        DialogueManager.Instance.NewDialogue();
        DialogueManager.Instance.AddDialogueSpeech(Speaker.Player, "A red door. Like the one at home. I remember...");
        DialogueManager.Instance.AddDialogueSpeech(Speaker.Mother, "My son! He's waking up!");
        DialogueManager.Instance.AddDialogueSpeech(Speaker.Player, "Mom! I remember now! How could I forget?");
        DialogueManager.Instance.AddDialogueSpeech(Speaker.Sandman, "Ggrraaahhh!");
        DialogueManager.Instance.StartDialogue();
    }

    private void Win()
    {
        SceneManager.LoadScene("Win");
    }
}
