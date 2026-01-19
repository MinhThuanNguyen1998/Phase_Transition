using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoAirPumpUIController : MonoBehaviour
{
    [SerializeField] private Sprite m_NormalSprite;
    [SerializeField] private Sprite m_PressedSprite;
    [SerializeField] private AirPumpMoving m_AirPumpMoving;
    [SerializeField] private Image m_AirPumpToggleImage;
    private bool m_IsToggleOn = false;
    public void ToggleHintMode()
    {
        m_IsToggleOn = !m_IsToggleOn;
        UpdateToggleUI();
        if (!m_IsToggleOn) StopSpawningMolecule();
        else StartSpawningMolecule();
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
    private void StartSpawningMolecule() => m_AirPumpMoving.StartAutoPump();
    private void StopSpawningMolecule() => m_AirPumpMoving.StopAutoPump();
}
