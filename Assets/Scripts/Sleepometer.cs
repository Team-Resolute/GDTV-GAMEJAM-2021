using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Sleepometer : MonoBehaviour
{
    [SerializeField] public float maxTimer = 300f;
    //[SerializeField] public string sceneToLoad;
    [SerializeField] public Level levelToLoad;
    [SerializeField] public Image sheets;
    [SerializeField] public TextMeshProUGUI timerUI;
    private float currentTimer = 0;
    private bool isActive = false;
    

    public void StartTimer()
    {
        isActive = true;
        currentTimer = maxTimer;
    }

    public void Update()
    {
        // Update time and bed sprite
        if(isActive == true)
        {
            currentTimer -= Time.deltaTime;
            if(currentTimer <= 0)
            {
                currentTimer = 0;
                //SceneManager.LoadScene(sceneToLoad);
                isActive = false;
                StartCoroutine(nameof(ResolveTimeOut));
            }
            else
            {
                int minutes = (int)(currentTimer / 60);
                int seconds = (int)(currentTimer - (minutes * 60));
                timerUI.text = minutes.ToString().PadLeft(2, '0') + ":" + seconds.ToString().PadLeft(2, '0');
                sheets.fillAmount = currentTimer / maxTimer;
            }
        }

    }

    private IEnumerator ResolveTimeOut()
    {
        Time.timeScale = 0f;
        for (int i = 0; i < 10; i++)
        {
            timerUI.enabled = !(timerUI.enabled);
            yield return new WaitForSecondsRealtime(0.25f);
        }
        Time.timeScale = 1f;
        FindObjectOfType<LevelChanger>().ChangeScene(levelToLoad);
    }
}
