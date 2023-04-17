using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UISetText : MonoBehaviour
{
    private TextMeshProUGUI _textUI;

    private void Awake()
    {
        _textUI = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        _textUI.alpha = 1;        
        _textUI.text = text;
    }

    public void HideText()
    {
        _textUI.alpha = 0;
    }
}
