using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoAirPumpUIController : MonoBehaviour
{
    [SerializeField] private Sprite m_NormalSprite;
    [SerializeField] private Sprite m_PressedSprite;
    [SerializeField] private Image m_AirPumpToggleImage;

    public static event Action<bool> OnAutoPumpToggle;
    private bool m_IsToggleOn = false;

    private void OnEnable() => AirPumpMoving.OnAutoPumpStopped += HandleAutoPumpStopped;
    private void OnDisable() => AirPumpMoving.OnAutoPumpStopped -= HandleAutoPumpStopped;
    private void HandleAutoPumpStopped() => ForeTurnOffAirPumpToggleOn();

    public void ToggleHintMode()
    {
        m_IsToggleOn = !m_IsToggleOn;
        UpdateToggleUI();
        OnAutoPumpToggle?.Invoke(m_IsToggleOn);
    }
    public void ForeTurnOffAirPumpToggleOn()
    {
        m_IsToggleOn = false;
        UpdateToggleUI();
    }
    private void UpdateToggleUI()
    {
        if (m_AirPumpToggleImage == null)
        {
            Debug.LogError("m_AirPumpToggleImage is null");
            return;
        }
        m_AirPumpToggleImage.sprite = m_IsToggleOn ? m_PressedSprite : m_NormalSprite;
    }
}
