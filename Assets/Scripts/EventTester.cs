using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventTester : MonoBehaviour
{
    [SerializeField] private int eventNumber;
    void Start()
    {
        CallEventByIndex();

    }

    private void CallEventByIndex()
    {
        EventManager.instance.CallEventByIndex(eventNumber);
    }


}
