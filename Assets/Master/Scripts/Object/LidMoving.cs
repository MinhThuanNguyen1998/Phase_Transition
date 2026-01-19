using UnityEngine;

public class LidMoving : BaseObjectMoving
{
    [SerializeField] private Renderer m_BoundaryCube;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        m_Bounds = m_BoundaryCube.bounds;
    }
    protected override Vector3 ClampPosition(Vector3 worldPos)
    {
        float y = Mathf.Clamp(worldPos.y, m_Bounds.min.y, m_Bounds.max.y);
        return new Vector3(transform.position.x, y, transform.position.z);
    }
}
