using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    private string currentHelpMessage = "Arraste a bicicleta para cima e para fora da caixa";
    private int errorCount = 0;
    [SerializeField] private GameObject errorUI;

    private void Awake()
    {
        EventManager.OnCallEvent += ResetErrorCount;
    }

    public void WrongMove()
    {
        EnableErrorUI();
        errorCount++;
        if (errorCount >= 3)
        {
            UISetText.Instance.SetText(currentHelpMessage);
            ResetErrorCount();
        }
    }

    private void ResetErrorCount()
    {
        errorCount = 0;
    }

    public void SetHelpText(string text)
    {
        currentHelpMessage = text;
        if (GameManager.Instance.IsGuidedMode == true)
        {
            UISetText.Instance.SetText(currentHelpMessage);
        }
        
    }

    public void EnableErrorUI()
    { 
        LeanTween.scale(errorUI, Vector2.one, 0.25f).setEase(LeanTweenType.easeOutElastic).setOnComplete(()=> 
        {
            LeanTween.scale(errorUI, Vector2.zero, 0.25f).setEase(LeanTweenType.easeInBack).setDelay(1.25f);

        });
    }

    private void OnDisable()
    {
        EventManager.OnCallEvent -= ResetErrorCount;
    }
}
