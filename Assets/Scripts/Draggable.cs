using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public static bool SomethingBeingDragged = false;
    private bool isDragging = false;


    [SerializeField] private UnityEvent OnDragStartEvent;
    [SerializeField] private UnityEvent OnDragEndEvent;

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

    public void OnDisable()
    {
        isDragging = false;
        SomethingBeingDragged = false;
    }


    private void Update()
    {
        if (isDragging)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 touchPosition = touch.position;
                    touchPosition.z = screenPoint.z;
                    Vector3 curPosition = Camera.main.ScreenToWorldPoint(touchPosition) + offset;
                    transform.position = curPosition;
                }
            }
            else
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                transform.position = curPosition;
            }
        }
    }
}
