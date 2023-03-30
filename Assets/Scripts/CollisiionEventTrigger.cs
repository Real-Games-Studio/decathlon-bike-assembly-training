using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class CollisiionEventTrigger : MonoBehaviour
{
    public string collisionTargetName;
    public UnityEvent OnTouchTarget;
    public UnityEvent OnTouchNonTarget;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Equals(collisionTargetName) == true)
        {
            OnTouchTarget?.Invoke();
            Debug.Log("Tocou no certo:"+ collision.transform.name);
        }
        else
        {
            Debug.Log(collision.transform.name);
            OnTouchNonTarget?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Equals(collisionTargetName) == true)
        {
            OnTouchTarget?.Invoke();
            Debug.Log("Tocou no certo:" + other.transform.name);
        }
        else
        {
            Debug.Log(other.transform.name);
            OnTouchNonTarget?.Invoke();
        }
    }


}
