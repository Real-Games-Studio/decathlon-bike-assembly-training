using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UISetText : MonoBehaviour
{
    public static UISetText Instance { get; private set; }
    private TextMeshProUGUI _textUI;
    [SerializeField] private RectTransform TextBoxUI;
    private Vector2 openPosition = new Vector2(-50, -50);
    private Vector2 closedPosition = new Vector2(1350, -50);


    private void Awake()
    {
        Instance = this;
        _textUI = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        LeanTween.move(TextBoxUI, openPosition, 0.3f).setEaseOutBack();
        _textUI.text = text;
    }

    public void HideText()
    {
        LeanTween.move(TextBoxUI, closedPosition, 0.3f).setEaseInBack();
    }

   
}
