using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CameraShakeData : ScriptableObject
{
    public AnimationCurve curve;
    public float duration = 1f;
    public float strengthMultiplier = 1f;
}
