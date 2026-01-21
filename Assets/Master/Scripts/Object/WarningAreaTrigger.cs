using UnityEngine;

public class WarningAreaTrigger : BaseTrigger
{
    [SerializeField] StateChangeController m_StateChangeController;

    protected override void OnEnter(Collider other)
    {
        if ((IsValidLid(other)))
        {
            Debug.Log("OnTrigger");
            m_StateChangeController.WarnIfMoleculesFull(true);
        }
    }
    private bool IsValidLid(Collider other)
    {
        return other.CompareTag("Needle");
    }
}
