using UnityEngine;

public class BottomFlaskTrigger : BaseTrigger
{
    [SerializeField] private StateChangeController m_StateChangeController;

    protected override void OnStay(Collider other)
    {
        if ((IsValidLid(other)))
        {
           // Debug.Log("Lid OnEnter");
            m_StateChangeController.WarnIfMoleculesFull(true);
        }
    }
    protected override void OnExit(Collider other)
    {
        if ((IsValidLid(other)))
        {
            //Debug.Log("Lid OnExit");
            m_StateChangeController.WarnIfMoleculesFull(false);
        }
    }
    private bool IsValidLid(Collider other)
    {
        return other.CompareTag("Lid") && TemperatureModel.TEMPERATURE >= TemperatureModel.MAX_TEMP;
    }
}
