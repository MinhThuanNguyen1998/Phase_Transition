using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CameraShakeEffect : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform m_CameraTarget;

    [Header("Shake Settings")]
     private float m_Duration = 2f;
     private float m_Strength = 0.1f;
     private int m_Vibrato = 20;
     private float m_Randomness = 90f;

    private Vector3 m_OriginalLocalPos;
    private Tween m_ShakeTween;

    private void Awake()
    {
        if (m_CameraTarget == null)
        {
            Debug.LogError("CameraShakeEffect: m_CameraTarget is NULL");
            enabled = false;
            return;
        }
        m_OriginalLocalPos = m_CameraTarget.localPosition;
    }
    public void Shake()
    {
        Shake(m_Duration, m_Strength);
    }
    public void Shake(float duration, float strength)
    {
        AudioMainManager.Instance.PlayOnShot(SoundType.Alert);
        m_ShakeTween?.Kill();
        m_CameraTarget.localPosition = m_OriginalLocalPos;
       
        m_ShakeTween = m_CameraTarget.DOShakePosition(
            duration,
            strength,
            m_Vibrato,
            m_Randomness,
            fadeOut: true
        ).OnComplete(() =>
        {
            MainManager.Instance.LoadExp();
            m_CameraTarget.localPosition = m_OriginalLocalPos;
            
        });
    }
    public void StopShake()
    {
        m_ShakeTween?.Kill();
        m_CameraTarget.localPosition = m_OriginalLocalPos;
    }
}
