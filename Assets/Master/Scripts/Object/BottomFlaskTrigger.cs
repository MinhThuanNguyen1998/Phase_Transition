using System;
using UnityEngine;

public class BottomFlaskTrigger : BaseTrigger
{
    public static event Action OnStateChangedUI;
    public static event Action<float> OnPointMoveRequested;
    protected override void OnEnter(Collider other)
    {
        if ((IsValidLid(other)))
        {
            TemperatureModel.SetGasState();
            OnStateChangedUI?.Invoke();
            OnPointMoveRequested?.Invoke(TemperatureModel.MAX_POINT_SPEED);
            //Debug.Log("Lid Trigger");
        }
    }
    protected override void OnExit(Collider other)
    {
        if ((IsValidLid(other)))
        {
            //Debug.Log("Lid Exit");
        }
    }
    private bool IsValidLid(Collider other)
    {
        return other.CompareTag("Lid");
    }
}
