using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseObjectMoving : MonoBehaviour
{
    protected bool m_IsDragging;
    protected Bounds m_Bounds;
    protected Vector3 m_Offset;

    protected virtual void Awake() { }
    protected virtual void Start() { }

    protected virtual void OnMouseDown()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        m_IsDragging = true;
    }

    protected virtual void OnMouseDrag()
    {
        if (!m_IsDragging) return;
        Move();
    }
    protected virtual void OnMouseUp()
    {
        m_IsDragging = false;
    }
    protected void Move() 
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        float y = Camera.main.ScreenToWorldPoint(mousePos).y + m_Offset.y;
        y = Mathf.Clamp(y, m_Bounds.min.y, m_Bounds.max.y);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
    protected abstract Vector3 ClampPosition(Vector3 worldPos);
}
