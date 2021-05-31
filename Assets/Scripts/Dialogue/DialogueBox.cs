using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    enum Status
    {
        None,
        Hidden,
        Hiding,
        Showing,
        Shown
    };

    private Status status = Status.Hidden;
    [SerializeField] private TextMeshProUGUI textbox;
    [SerializeField] private Image frame;
    [SerializeField] private Image backboard;
    [SerializeField] private Light backLight;
    private int lightIntensity = 10000;
    private int lightRange = 500;
    private bool operationDone = false;

    private Camera cam;
    // Update is called once per frame

    void Start()
    {
        cam = Camera.main;
        status = Status.Hidden;
        SetAlpha(0f);
    }

    private void SetAlpha(float alphaValue)
    {
        Color color;
        color = frame.material.color;
        color.a = alphaValue;
        frame.material.color = color;
        color = textbox.color;
        color.a = alphaValue;
        textbox.color = color;
        if (backboard)
        {
            color = backboard.color;
            color.a = Mathf.Clamp(alphaValue, 0f, 0.5f);
            backboard.color = color;
        }

        if (backLight)
        {
            Vector3 pos = cam.ScreenToWorldPoint(frame.rectTransform.position);
            pos = new Vector3( pos.x, pos.y, backLight.transform.position.z);
            backLight.transform.position = pos;
            backLight.intensity = (lightIntensity * alphaValue);
            backLight.range = (lightRange * alphaValue);
        }

    }

    

    void Update()
    {
        if (status == Status.Showing)
        {
            float alpha = frame.material.color.a;
            alpha += (1.3f * Time.unscaledDeltaTime);
            if (alpha >= 1f)
            {
                alpha = 1f;
                status = Status.Shown;
                operationDone = true;
            } 
            SetAlpha(alpha);

        }
        
        if (status == Status.Hiding)
        {
            float alpha = frame.material.color.a;
            alpha -= (1f * Time.unscaledDeltaTime);
            if (alpha <= 0f)
            {
                alpha = 0f;
                textbox.gameObject.SetActive(false);
                status = Status.Hidden;
                operationDone = true;
            } 
            SetAlpha(alpha);
        }
        
    }

    public void Show(DialogueSnippet snippet)
    {
        if (status != Status.Showing && status != Status.Shown)
        {
            textbox.gameObject.SetActive(true);
            textbox.text = snippet.speech;
            status = Status.Showing;
        }
    }

    public void Hide()
    {
        if (status != Status.Hidden && status != Status.Hiding)
        {
            status = Status.Hiding;
        }
    }

    public bool isOperationDone()
    {
        bool reportIsDone = operationDone;
        if (operationDone == true)
        {
            operationDone = false;
        }
        return reportIsDone;
    }
    
}
