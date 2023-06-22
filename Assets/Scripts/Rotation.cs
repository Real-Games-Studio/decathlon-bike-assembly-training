using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rotation : Movement
{
    public RotationAxis axis;
    public float rotationTarget;
    public bool isLoop;

}

public enum RotationAxis { X, Y, Z}

