using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
public class AirPumpMoving : BaseObjectMoving
{
    [SerializeField] private Renderer m_BoundaryCube;
    [SerializeField] private AutoAirPumpUIController m_UIController;
    [SerializeField] private float m_AutoPumpDuration = 0.5f;

    private Tween m_AutoPumpTween;
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
    protected override void OnMouseDown()
    {
       base.OnMouseDown();
       StopAutoPump();
    }
    public void StartAutoPump()
    {
        if (MoleculeManager.Instance.IsMoleculeOverLimit()) return;
        transform.position = new Vector3(transform.position.x, m_Bounds.max.y, transform.position.z);
        m_AutoPumpTween =
            transform.DOMoveY(m_Bounds.min.y, m_AutoPumpDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
    public void StopAutoPump()
    {
        if (m_AutoPumpTween != null)
        {
            m_UIController?.ForeTurnOffAirPumpToggleOn();
            m_AutoPumpTween.Kill();
        }
    }
}
