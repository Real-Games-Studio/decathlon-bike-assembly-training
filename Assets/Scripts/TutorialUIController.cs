using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI stepsText;

    public void SetTutorialText(TutorialText tutorialText)
    {
        titleText.text = tutorialText.tutorialTitle;
        stepsText.text = tutorialText.tutorialSteps;
    }

}
