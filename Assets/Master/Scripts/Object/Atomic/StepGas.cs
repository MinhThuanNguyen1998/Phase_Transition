using UnityEngine;

public class StepGas : StepAtomicBase
{
    private void OnEnable()
    {
        TotalSteps = 2;
        StartStep();
        Debug.Log("StartStepGas");
    }
    protected override void ExecuteCurrentStep()
    {
        switch (CurretSteps)
        {
            case 0:
                Debug.Log("Gas step 0: ");
                StepTutorialManager.Instance.GotoState(5);
                break;
            case 1:
                Debug.Log("Gas step 1: ");
                StepTutorialManager.Instance.GotoState(6);
                break;
        }
    }
}

