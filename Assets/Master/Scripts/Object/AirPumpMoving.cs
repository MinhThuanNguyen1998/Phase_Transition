using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
public class AirPumpMoving : BaseObjectMoving
{
    [SerializeField] private Renderer m_BoundaryCube;
    [SerializeField] private float m_AutoPumpDuration = 0.5f;
    public static event Action OnAutoPumpStopped;

    private void OnEnable() => AutoAirPumpUIController.OnAutoPumpToggle += HandleToggleFromUI;
 
    private void OnDisable() => AutoAirPumpUIController.OnAutoPumpToggle -= HandleToggleFromUI;

    private Tween m_AutoPumpTween;
    protected override void Start()
    {
        base.Start();
        m_Bounds = m_BoundaryCube.bounds;
    }
    private void HandleToggleFromUI(bool isOn)
    {
        if (isOn) StartAutoPump();
        else StopAutoPump();
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
        if (MoleculeManager.Instance.IsMoleculeOverLimit() || MoleculeManager.Instance.IsOverHeating) return;
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
            OnAutoPumpStopped?.Invoke();
            m_AutoPumpTween.Kill();
        }
    }
}
