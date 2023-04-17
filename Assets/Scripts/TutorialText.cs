using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialText", menuName = "ScriptableObjects/TutorialText")]
public class TutorialText : ScriptableObject
{
    public string tutorialTitle;


    [Multiline(10)] public string tutorialSteps;
}
