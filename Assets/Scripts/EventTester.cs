using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventTester : MonoBehaviour
{
    public enum TestEventBy
    {
        EventIndex,
        EventName

    }
    public TestEventBy testEventBy; 
    [SerializeField] private int eventNumber;
    [SerializeField] private string eventName;
    void Start()
    {
        switch (testEventBy)
        {
            case TestEventBy.EventIndex:
                CallEventByIndex();
                break;
            case TestEventBy.EventName:
                CallEventByName();
                break;
            default:
                break;
        }

    }

    private void CallEventByIndex()
    {
        EventManager.instance.CallEventByIndex(eventNumber);
    }

    private void CallEventByName()
    {
        EventManager.instance.CallEventByName(eventName);
    }


}
