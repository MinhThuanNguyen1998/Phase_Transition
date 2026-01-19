using UnityEngine;

public class AirPumpTrigger : BaseTrigger
{
    private bool m_IsFirstPump = false;
  
    protected override void OnEnter(Collider other)
    {
        if(!m_IsFirstPump) return;
        if (other.gameObject.tag == "AirPump")
        {
            MoleculeManager.Instance.SpawnMolecule();
        }
    }
    protected override void OnExit(Collider other)
    {
        if(other.gameObject.tag == "AirPump")
        {
            m_IsFirstPump = true;
        }
    }
   
}
