using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickDetection : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private float howManyTouchsToTrigger = 1;
    private float touchsDetected = 0;


    public UnityEvent OnTouch;
    public void OnPointerDown(PointerEventData eventData)
    {      
        touchsDetected++;
        if (touchsDetected >= howManyTouchsToTrigger)
        {
            OnTouch?.Invoke();
        }
    }
 
}
