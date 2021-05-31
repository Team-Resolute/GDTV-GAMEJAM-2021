using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum Level {StartMenu, Transition, Illusia, Tempus, Umbra, GameOver}

public class LevelChanger : MonoBehaviour
{
    private LevelChanger Instance = null;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }


    public void ChangeScene(Level level)
    {
        string levelName = Enum.GetName(typeof(Level), (int) level);
        SceneManager.LoadScene(levelName);
    }

    
}
