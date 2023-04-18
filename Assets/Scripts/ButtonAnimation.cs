using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
     

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private bool _enableButtonOnAnimationEnds = true;
    [SerializeField] private UnityEvent OnClick;

    private Vector2 buttonDefaultSize;
    private Button button;

    private float growAmount = 1.2f;
    private float animationTime = 0.15f;
    private void Awake()
    {
        buttonDefaultSize = gameObject.transform.localScale;
        button = GetComponent<Button>();
    }
    public void AnimateOnClick()
    {
        button.interactable = false;
        StartCoroutine(WaitToInvokeOnClickEvent());
        LeanTween.cancel(gameObject);

        float ajustedAnimationTime = animationTime;   
        LeanTween.scale(gameObject, buttonDefaultSize * growAmount, ajustedAnimationTime).setEaseOutCubic().setOnComplete(()=> 
        {
            LeanTween.scale(gameObject, buttonDefaultSize, ajustedAnimationTime).setEaseOutCubic().setOnComplete(EnableButton);
        });
    }

    private void EnableButton()
    {
        if (_enableButtonOnAnimationEnds == false) { return; }
        button.interactable = true;

    }

    private IEnumerator WaitToInvokeOnClickEvent()
    {
        yield return new WaitForSecondsRealtime(animationTime);
        OnClick?.Invoke();
    }
   
}
