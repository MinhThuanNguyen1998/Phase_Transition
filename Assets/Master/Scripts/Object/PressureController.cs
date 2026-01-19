using UnityEngine;

public class PressureController : MonoBehaviour
{
    [SerializeField] private Transform m_Needle;

    [Header("Angle Settings")]
    private const float m_MinAngle = -125f;
    private const float m_MaxAngle = 125f;
    private const float m_StartAngle = 125f;

    [Header("Movement")]
    private float m_Step = 48f;
    private float m_SmoothTime = 0.6f;

    [Header("Vibration (PhET style)")]
    private float m_VibrationStrength = 1.2f;
    private float m_VibrationSpeed = 30f;

    private float m_CurrentAngle;
    private float m_TargetAngle;
    private float m_Velocity;
    private float m_CurrentMinAngle;
    private void OnEnable()
    {
        StoveUIController.OnTemperatureChanged += HandleTemperatureChanged;
        StoveUIController.OnResetRequested += ResetNeedle;
    }
    private void OnDisable()
    {
        StoveUIController.OnTemperatureChanged -= HandleTemperatureChanged;
        StoveUIController.OnResetRequested -= ResetNeedle;
    }
    private void HandleTemperatureChanged(int delta)
    {
        IncreasePressureByTemperature(delta);
    }
    private void Start() => ResetNeedle();
    private void Update()
    {
        m_CurrentAngle = Mathf.SmoothDampAngle(m_CurrentAngle,m_TargetAngle,ref m_Velocity,m_SmoothTime);
        float angle = m_CurrentAngle;
        if (Mathf.Abs(m_CurrentAngle - m_TargetAngle) < 10f)
        {
            angle += Mathf.Sin(Time.time * m_VibrationSpeed) * m_VibrationStrength;
        }
        RotateNeedle(angle);
    }
    public void IncreasePressureByTemperature(int delta)
    {
        float direction = Mathf.Sign(delta);
        m_TargetAngle -= direction * m_Step;
        m_TargetAngle = Mathf.Clamp(m_TargetAngle, m_MinAngle, m_MaxAngle);
    }
    public void IncreasePressureByStep(float stepCount)
    {
        m_TargetAngle -= m_Step * stepCount;
        m_TargetAngle = Mathf.Clamp(m_TargetAngle, m_MinAngle, m_MaxAngle);
        Config.Radius += 1f;   
    }
    private void RotateNeedle(float angle)
    {
        Vector3 euler = m_Needle.localEulerAngles;
        m_Needle.localEulerAngles = new Vector3(euler.x, euler.y, angle);
    }
    public void ResetNeedle()
    {
        m_CurrentAngle = m_StartAngle;
        m_TargetAngle = m_StartAngle;
        m_Velocity = 0f;
        RotateNeedle(m_CurrentAngle);
    }
   
}
