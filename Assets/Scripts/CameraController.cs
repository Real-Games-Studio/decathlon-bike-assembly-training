using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;
    public float sensitivity = 5.0f;
    public float zoomSpeed = 2.0f;
    public float zoomSpeedMobile = 2.0f;
    public float minDistance = 3.0f;
    public float maxDistance = 20.0f;
    public float minYAngle = 5.0f;
    public float maxYAngle = 80.0f;
    private float x = 0.0f;
    private float y = 0.0f;
    private Vector3 lastPosition;
    private float lastDistance;

    private bool isAnimating = false;
    private bool canRotate = true;
    private Coroutine WaitToRotateCoroutine;
    private bool isWatingToStopDrag = false;
    private Coroutine WaitToStopDragCoroutine;

    private Vector2 lastMovements = Vector2.zero;
    private float zoomTarget;
    public Vector3 resetPosition;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

    }

    public void ChangeCameraTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Update()
    {

        if (isAnimating == true)
        {
            transform.LookAt(target);

            return;

        }
        if (Draggable.SomethingBeingDragged == true)
        {
            if (WaitToStopDragCoroutine != null)
            {
                StopCoroutine(WaitToStopDragCoroutine);

            }

            WaitToStopDragCoroutine = StartCoroutine(WaitToStopDrag());

            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            lastPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastPosition;
            x += delta.x * sensitivity * Time.deltaTime;
            y -= delta.y * sensitivity * Time.deltaTime;
            y = Mathf.Clamp(y, minYAngle, maxYAngle);
            lastPosition = Input.mousePosition;
        }
        else if (Input.touchCount == 1)
        {
            if (canRotate == true)
            {
                Vector2 touch1 = Input.GetTouch(0).position;
                Vector2 touch2 = Input.GetTouch(1).position;
                Vector2 delta = touch2 - touch1;
                x += delta.x * sensitivity * Time.deltaTime;
                y -= delta.y * sensitivity * Time.deltaTime;
                y = Mathf.Clamp(y, minYAngle, maxYAngle);
            }
            
                     
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.0f)
        {
            distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);
        }
        else if (Input.touchCount == 2)
        {
            if (WaitToRotateCoroutine != null)
            {
                StopCoroutine(WaitToRotateCoroutine);

            }
            WaitToRotateCoroutine = StartCoroutine(WaitToRotate());
            Vector2 touch1 = Input.GetTouch(0).position;
            Vector2 touch2 = Input.GetTouch(1).position;
            float distanceTouch = Vector2.Distance(touch1, touch2);
            if (lastDistance != 0.0f)
            {
                float deltaDistance = lastDistance - distanceTouch;
                distance = Mathf.Clamp(distance + deltaDistance * zoomSpeedMobile, minDistance, maxDistance);
            }
            lastDistance = distanceTouch;
        }
        else
        {
            lastDistance = 0.0f;
        }

        Vector2 newMovements = new Vector2(x, y);

        
        if (canRotate == true && (lastMovements != newMovements) && isWatingToStopDrag == false)
        {
            Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
        else
        {
            Vector3 position = transform.rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
            transform.position = position;
        }
        lastMovements = newMovements;




    }

    public void SetIsAnimating(bool boolean)
    {
        isAnimating = boolean;
    }

    private IEnumerator WaitToRotate()
    {
        canRotate = false;
        yield return new WaitForSeconds(0.3f);
        canRotate = true;
    }

    private IEnumerator WaitToStopDrag()
    {
        isWatingToStopDrag = true;
        yield return new WaitForSeconds(0.3f);
        isWatingToStopDrag = false;
    }

    public void ZoomByEvent(float newZoomValue)
    {
       
        LeanTween.value(gameObject, SetDistance, distance, newZoomValue, 0.6f).setEase(LeanTweenType.easeInOutSine).setOnComplete(StopCameraAnim);
    }

    private void SetDistance(float newZoomValue)
    {
        SetIsAnimating(true);
        Vector3 position = transform.rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
        transform.position = position;
        distance = newZoomValue;
    }

    private void StopCameraAnim()
    {
        SetIsAnimating(false);
    }

    public void GetResetPosition()
    {
        resetPosition = transform.position;
    }

    public void ResetCamera()
    {
        isAnimating = true;
        LeanTween.move(gameObject, resetPosition, 1).setOnComplete(()=> {isAnimating = false; });
        
    }
}