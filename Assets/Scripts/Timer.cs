using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float currentTimer = 0;
    private bool canRunTimer = false;
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private RectTransform TextBoxUI;
    private Vector2 openPosition = new Vector2(35, -35);
    private Vector2 closedPosition = new Vector2(-500, -50);
    public void StartTimer()
    {
        LeanTween.move(TextBoxUI, openPosition, 0.4f).setEaseOutBack();
        canRunTimer = true;
    }

    private void Update()
    {
        if (canRunTimer == true)
        {
            currentTimer += Time.deltaTime;
            ShowTimer();
        }     
        
    }

    public void StopTimer()
    {
        LeanTween.move(TextBoxUI, closedPosition, 0.4f).setEaseInBack();
        canRunTimer = false;
    }

    private void ShowTimer()
    {
        int minutes = Mathf.FloorToInt(currentTimer / 60);
        int seconds = Mathf.FloorToInt(currentTimer % 60);

        // Format the time into "00:00" clock format
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        TimerText.text = formattedTime;
    }


}
