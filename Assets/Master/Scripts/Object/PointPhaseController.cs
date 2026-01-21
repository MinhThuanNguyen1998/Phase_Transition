using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PointPhaseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform m_Point;
    [SerializeField] private RectTransform m_PointA;
    [SerializeField] private RectTransform m_PointB;
    [SerializeField] private float m_Step;
    private float m_Duration = 1f;
    private float m_ResetDuration = 8f;
    private float m_Time = 0f;
    private Tween m_MoveTween;
    private void Start() => ResetPoint();
    private void OnEnable()
    {
        StoveUIController.OnPointMoveRequested += Move;
        StoveUIController.OnResetRequested += ResetPoint;
        BottomFlaskTrigger.OnPointMoveRequested += Move;
    }
    private void OnDisable()
    {
        StoveUIController.OnPointMoveRequested -= Move;
        StoveUIController.OnResetRequested -= ResetPoint;
        BottomFlaskTrigger.OnPointMoveRequested -= Move;
    }
    public void Move(float step)
    {
        m_Time = Mathf.Clamp(m_Time + step, TemperatureModel.MIN_POINT_SPEED,TemperatureModel.MAX_POINT_SPEED);
        Vector3 targetPos = Vector3.Lerp(m_PointA.position, m_PointB.position, m_Time);
        m_MoveTween?.Kill();
        m_MoveTween = m_Point
            .DOMove(targetPos, m_Duration)
            .SetEase(Ease.OutCubic);
    }
    public void ResetPoint()
    {
        m_MoveTween?.Kill();
        m_MoveTween = null;
        m_Time = 0f;
        m_MoveTween = m_Point
      .DOMove(m_PointA.position, m_ResetDuration)
      .SetEase(Ease.OutCubic);
    }
}
