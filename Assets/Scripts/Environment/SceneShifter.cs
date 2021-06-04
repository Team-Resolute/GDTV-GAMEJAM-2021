using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SceneShifter : MonoBehaviour
{
    [SerializeField] private List<GameObject> shifts = new List<GameObject>();
    
    [SerializeField] private List<AudioClip> shiftedAudio = new List<AudioClip>();
    
    [SerializeField] private List<GameObject> shiftedLight = new List<GameObject>();
    [SerializeField] private AudioSource musicPlayer = default;
    private GameObject currentShift = default;
    private GameObject currentLight = default;

    //[SerializeField] private List<Collider> shiftColliders = new List<Collider>();

    public static SceneShifter Instance = null;
    

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

    void Start()
    {
        if (shifts.Count > 0)
        {
            foreach (GameObject shift in shifts)
            {
                shift.SetActive(false);
            }

            currentShift = shifts[0];
            currentShift.SetActive(true);
        }

        if (musicPlayer && shiftedAudio.Count > 0)
        {
            musicPlayer.clip = shiftedAudio[0];
            musicPlayer.Play();
        }
        
        if (shiftedLight.Count > 0)
        {
            shiftedLight[0].SetActive(true);
            currentLight = shiftedLight[0];
        }
        
    }

    public void Shift(int shiftNumber)
    {
        if (shiftNumber < shifts.Count)
        {
            currentShift.SetActive(false);
            currentShift = shifts[shiftNumber];
            currentShift.SetActive(true);
        }
        
        if (shiftNumber < shiftedAudio.Count)
        {
            musicPlayer.Stop();
            musicPlayer.clip = shiftedAudio[shiftNumber];
            musicPlayer.Play();
        }

        if (shiftNumber < shiftedLight.Count)
        {
            currentLight.SetActive(false);
            currentLight = shiftedLight[shiftNumber];
            currentLight.SetActive(true);
        }
    }

    
}
