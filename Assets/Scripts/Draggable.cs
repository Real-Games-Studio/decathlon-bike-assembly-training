using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public static bool SomethingBeingDragged = false;
    private bool isDragging = false;

    [SerializeField] private UnityEvent OnDragStartEvent;
    [SerializeField] private UnityEvent OnDragEndEvent;

    [SerializeField] private bool lockXAxis = false;
    [SerializeField] private bool lockYAxis = false;
    [SerializeField] private bool lockZAxis = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDragStartEvent?.Invoke();
        isDragging = true;
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, screenPoint.z));
        SomethingBeingDragged = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        SomethingBeingDragged = false;
        OnDragEndEvent?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = new Vector3(eventData.position.x, eventData.position.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            // Lock the specified axes
            if (lockXAxis)
                curPosition.x = transform.position.x;
            if (lockYAxis)
                curPosition.y = transform.position.y;
            if (lockZAxis)
                curPosition.z = transform.position.z;

            transform.position = curPosition;
        }
    }
}

// Enum for axis locking
public enum Axis
{
    None,
    X,
    Y,
    Z
}

