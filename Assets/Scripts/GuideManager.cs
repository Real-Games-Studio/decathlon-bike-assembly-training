using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI topMessage;
    [SerializeField] private TextMeshProUGUI bellowMessage;
    [SerializeField] private RectTransform GuideTextBoxUI;
    private Vector2 openPosition = new Vector2(20, -30);
    private Vector2 closedPosition = new Vector2(-1000, -30);

    public void SetGuideText(GuideText guideText)
    {
        topMessage.text = guideText.StepTitle;
        bellowMessage.text = guideText.Steps;

    }

    public void ShowGuideMessages()
    {
        LeanTween.move(GuideTextBoxUI, openPosition, 0.4f).setEaseOutBack();
    }
}
