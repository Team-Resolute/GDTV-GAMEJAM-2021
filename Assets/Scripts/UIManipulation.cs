using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManipulation : MonoBehaviour
{
    [SerializeField] Image fade = null;
    [SerializeField] float fadeSpeed = 0.5f;

    public void Start()
    {
        StartCoroutine(FadeBlackOutSquare());
    }

    public IEnumerator FadeBlackOutSquare()
    {
        float fadeAmount;
        Color objectColor = fade.color;

        while(fade.color.a > 0)
        {
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            fade.color = objectColor;
            yield return null;
        }
    }

}
