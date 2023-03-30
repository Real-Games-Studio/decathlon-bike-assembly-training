using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Moveable : MonoBehaviour
{
    public UnityEvent OnMove;
    public UnityEvent OnStop;

    private Movement currentMovement;
    public void Move(Movement movement)
    {
        OnMove?.Invoke();
        movement.OnMovementStart?.Invoke();
        LeanTween.move(gameObject, movement.transform.position, movement.time).setEase(movement.easing).setOnComplete(OnFinishMovement);
        currentMovement = movement;
    }

    public void ResetPosition()
    {
        OnMove?.Invoke();
        LeanTween.moveLocal(gameObject, Vector3.zero, 0).setOnComplete(OnFinishMovement);
    }

    private void OnFinishMovement()
    {
        OnStop?.Invoke();
        if (currentMovement != null)
        {
            currentMovement.OnMovementEnds?.Invoke();
        }
        


    }
}
