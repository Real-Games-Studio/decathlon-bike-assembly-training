using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    [SerializeField] protected RectTransform openTargetTransform;
    [SerializeField] protected Vector2 openPosition;
    [SerializeField] protected RectTransform closeTargetTransform;
    [SerializeField] protected Vector2 closePosition;
    [SerializeField] protected Vector2 openScale;
    [SerializeField] protected Vector2 closedScale;
    [SerializeField] protected float closedAlpha;
    [SerializeField] protected LeanTweenType openEasingType;
    [SerializeField] protected LeanTweenType closeEasingType;
    [Space(10)]
    [SerializeField] protected bool closeOnStart;


    private RectTransform _rectTransform;
    protected float animationTime = 0.45f;
    protected bool isOpen = true;
    protected bool isAnimating = false;

    protected CanvasGroup canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();

    }

    private void Start()
    {
        if (closeOnStart == true)
        {
            HideMenu();
        }
    }

    public void HideMenu()
    {
        isOpen = true;
        OnCloseMenu();
        if (canvasGroup != null)
        {
            if (closeTargetTransform == null)
            {
                AnimatePanel(closePosition, closedScale, closedAlpha, closeEasingType);
            }
            else
            {
                AnimatePanel(closeTargetTransform.localPosition, closedScale, closedAlpha, closeEasingType);
            }
           
                               
        }
        

    }

    public void ShowMenu()
    {
        isOpen = false;
        if (openTargetTransform == null)
        {
            AnimatePanel(openPosition, openScale, 1, openEasingType);
        }
        else
        {
            AnimatePanel(openTargetTransform.position, openScale, 1, openEasingType);
        }
        
        OnOpenMenu();
    }

    protected void AnimatePanel(Vector2 position, Vector2 scale, float alpha, LeanTweenType easingType = LeanTweenType.notUsed)
    {
        float ajustedAnimationTime = animationTime;
        LeanTween.move(_rectTransform, position, ajustedAnimationTime).setEase(easingType);
        LeanTween.scale(_rectTransform, scale, ajustedAnimationTime).setEase(easingType);
        LeanTween.alphaCanvas(canvasGroup, alpha, ajustedAnimationTime).setEase(easingType);
        
    }


    protected IEnumerator AnimationCouter()
    {
        isAnimating = true;
        yield return new WaitForSecondsRealtime(animationTime);
        isAnimating = false;
    }

    protected virtual void OnOpenMenu()
    {

    }

    protected virtual void OnCloseMenu()
    {

    }

    protected void ShowMenuWithDelay()
    {
        StartCoroutine(ShowMenuCounter());
    }

    private IEnumerator ShowMenuCounter()
    {
        float timeToShowMenu = 2;
        yield return new WaitForSeconds(timeToShowMenu);
        ShowMenu();
    }

}
