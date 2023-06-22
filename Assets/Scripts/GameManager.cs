using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] public bool IsGuidedMode { get; private set; }

    private void Awake()
    {
        Instance = this;
        IsGuidedMode = true;
    }
    public void Start()
    {
        
    }

    public void SetGuidedModeOn()
    {
        IsGuidedMode = true;
    }

    public void SetGuidedModeOff()
    {
        IsGuidedMode = false;
    }

}

