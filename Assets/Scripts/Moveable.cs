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

    public void Rotate(Rotation rotate)
    {
        OnMove?.Invoke();
        rotate.OnMovementStart?.Invoke();
       
        switch (rotate.axis)
        {
        
            case RotationAxis.X:
                if (rotate.isLoop == true)
                {
                    LeanTween.rotateAroundLocal(gameObject, Vector3.right, rotate.rotationTarget, rotate.time).setEase(rotate.easing).setRepeat(-1).setOnComplete(OnFinishMovement);
                }
                else
                {
                    LeanTween.rotateAroundLocal(gameObject, Vector3.right, rotate.rotationTarget, rotate.time).setEase(rotate.easing).setOnComplete(OnFinishMovement);
                }
                
                break;
            case RotationAxis.Y:
                if (rotate.isLoop == true)
                {
                    LeanTween.rotateAroundLocal(gameObject, Vector3.up, rotate.rotationTarget, rotate.time).setEase(rotate.easing).setRepeat(-1).setOnComplete(OnFinishMovement);
                }
                else
                {
                    LeanTween.rotateAroundLocal(gameObject, Vector3.up, rotate.rotationTarget, rotate.time).setEase(rotate.easing).setOnComplete(OnFinishMovement);
                }

               

                break;
            case RotationAxis.Z:
                if (rotate.isLoop == true)
                {
                    LeanTween.rotateAroundLocal(gameObject, Vector3.forward, rotate.rotationTarget, rotate.time).setEase(rotate.easing).setRepeat(-1).setOnComplete(OnFinishMovement);
                }
                else
                {
                    LeanTween.rotateAroundLocal(gameObject, Vector3.forward, rotate.rotationTarget, rotate.time).setEase(rotate.easing).setOnComplete(OnFinishMovement);
                }

                

                break;
        
        }

        currentMovement = rotate;
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

    public void StopLoop()
    {
        //OnFinishMovement();
        LeanTween.cancel(gameObject);
    }
}
