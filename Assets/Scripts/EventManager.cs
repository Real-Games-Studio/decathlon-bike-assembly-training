using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    
    public static EventManager instance;
    [SerializeField] private bool CallOnStart = false;
    [SerializeField] private List<GameEvent> EventSequence;
    private int currentEventIndex = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (CallOnStart == true)
        {
            CallNextEvent();
        }
        
    }

    private void CallEvent()
    {
        GameEvent currentEvent = EventSequence[currentEventIndex];
        Debug.Log(currentEvent.eventName);
        StartCoroutine(CounterToCallNextEvent(currentEvent.eventFunction, currentEvent.timeToRunEvent, currentEvent.CallNextEventAuto));
    }

    public void CallNextEvent()
    {
        Debug.Log("fdfs");
        if (currentEventIndex < EventSequence.Count)
        {
            GameEvent currentEvent = EventSequence[currentEventIndex];
            if (currentEvent.isIndepententEvent == true)
            {
                currentEventIndex++;
                CallNextEvent();
                return;
            }
            CallEvent();
            
        }
    }

    public void JumpEvents(int amoutOfJumps)
    {
        int newCurrentEventIndex = currentEventIndex + (amoutOfJumps + 1);
        if ( newCurrentEventIndex >= EventSequence.Count)
        {
            Debug.Log("Event jump get index out of event's list size");
            return;
        }

        currentEventIndex = newCurrentEventIndex;
        CallEvent();


    }

    public void CallEventByIndex(int index)
    {
        if (index >= EventSequence.Count)
        {
            Debug.Log("There is no event for this index");
            return;
        }

        currentEventIndex = index;
        CallEvent();
    }

    public void CallEventByName(string calledEventName)
    {
        for (int i = 0; i < EventSequence.Count; i++)
        {
            if (EventSequence[i].eventName.Equals(calledEventName) == true)
            {
                currentEventIndex = i;
                CallEvent();
                return;
            }
        }

        Debug.Log("Event name not found");
        
        
    }

    private IEnumerator CounterToCallNextEvent(UnityEvent newEvent, float time, bool callNextEventAuto)
    {

        yield return new WaitForSeconds(time);
        currentEventIndex++;
        newEvent.Invoke();
        if (callNextEventAuto == true)
        {
            CallNextEvent();
        }

    }
}

[System.Serializable]
public class GameEvent
{
    [SerializeField] public string eventName;
    [SerializeField] public UnityEvent eventFunction;
    [SerializeField] public float timeToRunEvent;
    [SerializeField] public bool CallNextEventAuto = false;
    [SerializeField] public bool isIndepententEvent = false;

}


