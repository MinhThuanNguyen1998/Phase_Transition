using System;
using UnityEngine;

public class LidMoving : BaseObjectMoving
{
    [SerializeField] private Renderer m_BoundaryCube;
    public static event Action<float> OnCompressionChanged;
    private Transform m_BottomFlask;
    private float m_MaxDistance;
    
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        m_Bounds = m_BoundaryCube.bounds;
        GameObject flask = GameObject.FindGameObjectWithTag("BottomFlask");
        if (flask != null)
        {
            m_BottomFlask = flask.transform;
            m_MaxDistance = transform.position.y - m_BottomFlask.position.y;
        }
        else Debug.LogError("BottomFlask not found! Check tag name.");
    }
    protected override void OnMouseDrag()
    {
        base.OnMouseDrag();
        float distance = GetDistanceY();
        float compression = Mathf.Clamp01(1f - distance / m_MaxDistance);
        OnCompressionChanged?.Invoke(compression);
    }
    protected override Vector3 ClampPosition(Vector3 worldPos)
    {
        float y = Mathf.Clamp(worldPos.y, m_Bounds.min.y, m_Bounds.max.y);
        return new Vector3(transform.position.x, y, transform.position.z);
    }
    private float GetDistanceY()
    {
        return Mathf.Abs(transform.position.y - m_BottomFlask.position.y);
    }
}
