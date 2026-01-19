using UnityEngine;

[System.Serializable]
public class PressureThreshold
{
    public int moleculeCount;
    public float pressureStep;
    [HideInInspector] public bool triggered;
}
