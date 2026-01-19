using UnityEngine;

public abstract class BaseTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        OnEnter(other);
        //Debug.Log(other + "OnTriggerEnter");
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit(other);
        //Debug.Log(other + "OnTriggerExist");
    }

    private void OnTriggerStay(Collider other)
    {
        OnStay(other);
        //Debug.Log(other + "OnTriggerStay");
    }
    protected virtual void OnEnter(Collider other) {}
    protected virtual void OnExit(Collider other) {}
    protected virtual void OnStay(Collider other) {}
}
