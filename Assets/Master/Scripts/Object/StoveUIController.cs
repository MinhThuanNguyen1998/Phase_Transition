using System;
using TMPro;
using UnityEngine;
public enum TemperatureUnit
{
    Celsius,
    Kelvin
}
public class StoveUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextTemperature;

    public static event Action OnResetRequested;
    public static event Action<int> OnTemperatureChanged; // delta
    public static event Action OnStateChanged;
    public static event Action<float> OnPointMoveRequested;

    private const float DEFAULT_FONT_SIZE = 48f;
    private int m_TemperatureValue = 20;
    private bool m_IsOn = false;
    private bool m_HasSwitchedMode = false;
    private TemperatureUnit m_CurrentUnit = TemperatureUnit.Celsius;
    private void Start() => ResetDefaultValue();
    public void OnButtonOn_Of()
    {
        PlayAudioBeep();
        ToggleOn_Off();
    }
    public void OnButtonPlus()
    {
        ChangeTemperature(+Config.STEP_TEMP);
        OnPointMoveRequested?.Invoke(+Config.POINT_SPEED_ON_CHANGE_TEMPERATURE);
    }
    public void OnButtonMinus() 
    {
        ChangeTemperature(-Config.STEP_TEMP);
        OnPointMoveRequested?.Invoke(-Config.POINT_SPEED_ON_CHANGE_TEMPERATURE);
    } 
    public void OnButtonSwitchMode() 
    {
        if (!CanInteract()) return;
        m_HasSwitchedMode = true;
        m_CurrentUnit = m_CurrentUnit == TemperatureUnit.Celsius? TemperatureUnit.Kelvin: TemperatureUnit.Celsius;
        Config.OnTemperatureChanged(Config.MIN_TEMP);
        OnPointMoveRequested?.Invoke(Config.POINT_SPEED_ON_SWITCH_MODE);
        UpdateTemperatureText();
    }
    private void ToggleOn_Off()
    {
        m_IsOn = !m_IsOn;
        if (!m_IsOn)
        {
            ResetDefaultValue();
            return;
        }
        m_TextTemperature.fontSize = DEFAULT_FONT_SIZE;
        m_TextTemperature.text = Config.DEFAULT_TEXT;
    }
    private void ChangeTemperature(int delta)
    {
        if (!CanChangeTemperature()) return;
        m_TemperatureValue = Mathf.Clamp(m_TemperatureValue + delta,Config.MIN_TEMP,Config.MAX_TEMP);
        OnTemperatureChanged?.Invoke(delta);
        Config.OnTemperatureChanged(delta);
        Config.TEMPERATURE = m_TemperatureValue;
        UpdateTemperatureText();
    }
    private void UpdateTemperatureText()
    {
        int value = m_CurrentUnit == TemperatureUnit.Kelvin? m_TemperatureValue + 273: m_TemperatureValue;
        string unit = m_CurrentUnit == TemperatureUnit.Kelvin? Config.Temperature_Kelvin: Config.Temperature_Celcius;
        m_TextTemperature.text = value + unit;
        OnStateChanged?.Invoke();
    }
    private void ResetDefaultValue()
    {
        m_IsOn = false;
        m_HasSwitchedMode = false;
        m_CurrentUnit = TemperatureUnit.Kelvin;
        m_TemperatureValue = Config.MIN_TEMP;
        m_TextTemperature.fontSize = 0f;
        Config.Radius = Config.DefaultRadius;
        OnStateChanged?.Invoke();
        OnResetRequested?.Invoke();
    }
    private bool CanInteract()
    {
        if (!m_IsOn) return false;
        PlayAudioBeep();
        return true;
    }
    private bool CanChangeTemperature()
    {
        if(!m_HasSwitchedMode) return false;
        PlayAudioBeep() ;
        return true;
    }
    private void PlayAudioBeep() => AudioMainManager.Instance.PlayOnShot(SoundType.Beep);
}
