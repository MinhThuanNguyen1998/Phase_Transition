using UnityEngine;

public class MoleculeMotion : MonoBehaviour
{
    private Vector3 m_OriginPosition; // Origin keeps the molecule vibrating around a fixed equilibrium position
    private float m_NoiseOffset;
    private BoxCollider m_Bound;
    private float m_OffsetBoundX = 0.6f;
    private bool m_IsInitialized = false;
    private float m_SmoothSpeedDefaultRadius = 0.8f;
    public void Initialize(Vector3 origin)
    {
        m_OriginPosition = origin;
        m_Bound = MoleculeManager.Instance.Bound;
        m_IsInitialized = true;
        m_NoiseOffset = Random.Range(0f, 100f);
    }
    public void SetOrigin(Vector3 origin) => m_OriginPosition = origin;
    
    private void Update() 
    {
        if (!m_IsInitialized || m_Bound == null) return;
        UpdateMoleculeMotion();
    }
    private void UpdateMoleculeMotion()
    {
        Bounds bounds = m_Bound.bounds;
        float time = Time.time + m_NoiseOffset;
        Vector3 offset = new Vector3(Mathf.PerlinNoise(time, 0f) - 0.5f, Mathf.PerlinNoise(0f, time) - 0.5f, 0f) * Config.Radius;
        Vector3 target = m_OriginPosition + offset;
        target.x = Mathf.Clamp(target.x, bounds.min.x + m_OffsetBoundX, bounds.max.x);
        target.y = Mathf.Clamp(target.y, bounds.min.y, bounds.max.y);
        if (Mathf.Approximately(Config.Radius, Config.DefaultRadius))
        {
            transform.position = Vector3.Lerp(
                transform.position,
                target,
                Time.deltaTime * m_SmoothSpeedDefaultRadius
            );
        }
        else transform.position = target;
    }
}
