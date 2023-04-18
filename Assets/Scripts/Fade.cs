using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public static Fade instance;
    [SerializeField] private bool FadeInOnStart;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (FadeInOnStart == true)
        {
            FadeIn();
        }

    }
    public void FadeIn(float fadeTime = 2)
    {
        LeanTween.cancel(gameObject);
        LeanTween.alphaCanvas(canvasGroup, 0, fadeTime).setEaseInOutSine();
        
        
    }

    public void FadeOut(float fadeTime = 2)
    {
        LeanTween.cancel(gameObject);
        LeanTween.alphaCanvas(canvasGroup, 1, fadeTime).setEaseInOutSine();

    }

 
}
