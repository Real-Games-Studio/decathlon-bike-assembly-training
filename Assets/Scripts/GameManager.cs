using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static Action<bool> OnSetMode;
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
        OnSetMode?.Invoke(IsGuidedMode);
    }

    public void SetGuidedModeOff()
    {
        IsGuidedMode = false;
        OnSetMode?.Invoke(IsGuidedMode);
    }

}

