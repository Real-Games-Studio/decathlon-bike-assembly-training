using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GuideText", menuName = "ScriptableObjects/GuideText")]
public class GuideText : ScriptableObject
{
    [TextArea(0,50)]
    public string StepTitle;
    [TextArea(0, 50)]
    public string Steps;
    public Sprite guideSprite;


}
