using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI pointsTextUI;
    private int points;

    public void Awake()
    {
        Instance = this;
        EventManager.OnCallNonAutoEvent += Gain50Points;
    }

    private void UpdatePointsUI()
    {
        pointsTextUI.text = points.ToString() + "pts";
    }

    public void GainPoints(int pointsGained)
    {
        points += pointsGained;
        UpdatePointsUI();
    }

    public void LoosePoints(int pointsLost)
    {
        points -= pointsLost;
        if (points < 0)
        {
            points = 0;
        }

        UpdatePointsUI();
    }

    private void Gain50Points()
    {
        GainPoints(50);
    }

    public void OnDisable()
    {
        EventManager.OnCallNonAutoEvent -= Gain50Points;
    }



}
