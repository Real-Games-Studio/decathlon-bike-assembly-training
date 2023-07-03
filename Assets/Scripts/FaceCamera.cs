using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 desiredScale = new Vector3(0.2f, 0.2f, 0.2f);
    private bool alreadyAjusted = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        GameManager.OnSetMode += AjustMarker;
        if (GameManager.Instance == null)
        {
            StartCoroutine(WaitToEnable());
            return;
        }
        AjustMarker(GameManager.Instance.IsGuidedMode);
    }
    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    private IEnumerator WaitToEnable()
    {
        yield return new WaitForSeconds(1);
        AjustMarker(GameManager.Instance.IsGuidedMode);

    }

    private void AjustMarker(bool IsGuidedMode)
    {
        if (alreadyAjusted == true)
        {
            return;
        }

        alreadyAjusted = true;

        spriteRenderer.enabled = IsGuidedMode;

        Vector3 currentGlobalScale = transform.lossyScale;
        Vector3 localScale = new Vector3(desiredScale.x / currentGlobalScale.x, desiredScale.y / currentGlobalScale.y, desiredScale.z / currentGlobalScale.z);
        transform.localScale = localScale;

    }

    private void OnDisable()
    {
        GameManager.OnSetMode -= AjustMarker;
    }
}
