using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingObjectByMouse : MonoBehaviour
{
    [SerializeField] private Renderer m_BoundaryCube;
    [SerializeField] private bool m_UseSpecialYLimit;
    [SerializeField] private DragCategory m_DragCategory = DragCategory.None;
    private Vector3 m_Offset;
    public bool m_IsDragging = false;
    private Bounds m_Bounds;
    [SerializeField] private float m_BoundYOffset = 1.5f;

    private void Awake()
    {
        if (m_BoundaryCube == null)
        {
            GameObject boundary = GameObject.FindGameObjectWithTag("Boundary");
            if (boundary != null) m_BoundaryCube = boundary.GetComponent<Renderer>();
            else Debug.Log("Can not find Object with tag name is Boundary");
        }
    }
    private void Start()
    {
        if (m_BoundaryCube != null) m_Bounds = m_BoundaryCube.bounds;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        if (MouseDragLock.IsBlocked && m_DragCategory != DragCategory.None)return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z; 
        m_Offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
        m_IsDragging = true;
    }
    private void OnMouseDrag()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        if (MouseDragLock.IsBlocked && m_DragCategory != DragCategory.None) return;
        if (m_IsDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition) + m_Offset;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(newPosition);
            newPosition = Camera.main.ScreenToWorldPoint(screenPos);
            newPosition.x = Mathf.Clamp(newPosition.x, m_Bounds.min.x, m_Bounds.max.x);
            if(m_UseSpecialYLimit) newPosition.y = Mathf.Clamp(newPosition.y, m_Bounds.min.y + m_BoundYOffset, m_Bounds.max.y);
            else newPosition.y = Mathf.Clamp(newPosition.y, m_Bounds.min.y, m_Bounds.max.y);
            transform.position = newPosition;
        }
    }
    private void OnMouseUp() => m_IsDragging = false;
}

