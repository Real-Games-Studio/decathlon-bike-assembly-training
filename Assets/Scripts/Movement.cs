using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    public float time;
    public LeanTweenType easing;

    public UnityEvent OnMovementStart;
    public UnityEvent OnMovementEnds;

}
