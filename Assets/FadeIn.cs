using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private float totalFadeTime = 12f;
    [SerializeField] private MeshRenderer meshRendererToFade = default;

    private float startAlpha = 1.2f;
    private float endAlpha = 0f;
    private float t = 0f;
    private float time = 0f;
    private float a = default;
    void Start()
    {
        Color color = meshRendererToFade.material.color;
        color.a = startAlpha;
        a = startAlpha;
        meshRendererToFade.material.color = color;
    }
    void Update()
    {
        if (a <= 0) { return;}  
        
        if (a > endAlpha && totalFadeTime > 0f)
        {
            t = time / totalFadeTime;
            time += Time.deltaTime;
            a = Mathf.Lerp(startAlpha, endAlpha, t);
            if (a < endAlpha) { a = endAlpha; }
            Color color = meshRendererToFade.material.color;
            color.a = Mathf.Clamp01(a);
            meshRendererToFade.material.color = color;
        }
    }
}
