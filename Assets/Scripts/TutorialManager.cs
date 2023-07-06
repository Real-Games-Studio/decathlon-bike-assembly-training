using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Menu NextButton;
    [SerializeField] private Menu GuideMenu;
    [SerializeField] private Menu InfoMenu;
    [SerializeField] private Menu TutorialUI;
    [SerializeField] private GameObject Bike;
    [SerializeField] private GameObject BikeHolder;
    [SerializeField] private TextMeshProUGUI TitleGuide;
    [SerializeField] private TextMeshProUGUI StepsGuide;
    [SerializeField] private Image GuideSprite;

    [SerializeField] private GuideText CameraGuideText;
    [SerializeField] private GuideText CameraZoomGuideText;
    [SerializeField] private GuideText CameraHoldText;

    private TutorialPhase tutorialPhase;

    public void StartTutorial()
    {
        tutorialPhase = TutorialPhase.Rotate;
        ChangeTutorialGuideText(CameraGuideText);
        GuideMenu.ShowMenu();
        InfoMenu.ShowMenu();
        TutorialUI.HideMenu();
        Bike.SetActive(true);
        StartCoroutine(WaitToShowButton());

    }

    private IEnumerator WaitToShowButton()
    {
        yield return new WaitForSeconds(5);
        NextButton.ShowMenu();
    }

    public void EndTutorial()
    {
        GuideMenu.HideMenu();
        InfoMenu.HideMenu();
        NextButton.HideMenu();
        TutorialUI.ShowMenu();
        Bike.SetActive(false);
        BikeHolder.SetActive(false);
    }

    public void ChangeTutorialGuideText(GuideText newGuideText)
    {
        TitleGuide.text = newGuideText.StepTitle;
        StepsGuide.text = newGuideText.Steps;
        GuideSprite.sprite = newGuideText.guideSprite;

    }

    private void StartZoomPhase()
    {
        tutorialPhase = TutorialPhase.Zoom;
        ChangeTutorialGuideText(CameraZoomGuideText);
        NextButton.HideMenu();
        StartCoroutine(WaitToShowButton());
    }

    private void StartHoldPhase()
    {
        tutorialPhase = TutorialPhase.Hold;
        ChangeTutorialGuideText(CameraHoldText);
        NextButton.HideMenu();
        StartCoroutine(WaitToShowButton());
        Bike.SetActive(false);
        BikeHolder.SetActive(true);
    }

    public void StartNewPhase()
    {
        switch (tutorialPhase)
        {
            case TutorialPhase.Rotate:
                StartZoomPhase();
                break;
            case TutorialPhase.Zoom:
                StartHoldPhase();
                break;
            case TutorialPhase.Hold:
                EndTutorial();
                break;
            default:
                break;
        }
    }

    private enum TutorialPhase
    {
        Rotate,
        Zoom,
        Hold

    }
}
