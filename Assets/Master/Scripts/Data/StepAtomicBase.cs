using UnityEngine;
using UnityEngine.Rendering;

public abstract class StepAtomicBase : MonoBehaviour
{
    public int TotalSteps { get; protected set; }
    public int CurretSteps { get; protected set; }

    public virtual void StartStep()
    {
        CurretSteps = 0;
        ExecuteCurrentStep();
    }
    public void GoToNextStep()
    {
        NextStep();
    }
    public void GoToPrevStep()
    {
        PrevStep();
    }
    protected void PrevStep()
    {
        if (CurretSteps <= 0)
        {
            return;
        }
        CurretSteps--;
        if (CurretSteps >= 1)
        {
            ExecuteCurrentStep();
        }
        else StartStep();
    }
    protected void NextStep()
    {
        CurretSteps++;
        if(CurretSteps <= TotalSteps) ExecuteCurrentStep();
        else OnAllStepCompleted();
    }
    protected abstract void ExecuteCurrentStep();
    
    protected virtual void OnAllStepCompleted() { }
}
