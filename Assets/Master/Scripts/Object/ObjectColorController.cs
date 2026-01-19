using UnityEngine;
using UnityEngine.UI;
public class ObjectColorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FlexibleColorPicker m_Fcp;
    [SerializeField] private Renderer m_Renderer;
    [SerializeField] private Button m_ResetDefaultMaterialButton;
    [Header("Default")]
    [SerializeField] private Material m_DefaultMaterial;
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    private Material m_RuntimeMaterial;
    private string SaveKey => $"OBJECT_COLOR_{gameObject.name}";
    private void Awake()
    {
        m_RuntimeMaterial = new Material(m_Renderer.sharedMaterial);
        m_Renderer.material = m_RuntimeMaterial;
    }
    private void Start()
    {
        Color startColor = LoadColor();
        ApplyColor(startColor);
        m_Fcp.color = startColor;
        m_Fcp.onColorChange.AddListener(OnChangeColor);
        if (m_ResetDefaultMaterialButton != null) m_ResetDefaultMaterialButton.onClick.AddListener(OnResetDefault);
    }
    private void OnDestroy() => m_Fcp.onColorChange.RemoveListener(OnChangeColor);
    private void OnChangeColor(Color color)
    {
        ApplyColor(color);
        SaveColor(color);
    }
    private void OnResetDefault()
    {
        if (m_DefaultMaterial == null) return;
        if (m_DefaultMaterial.HasProperty(BaseColor))
        {
            Color defaultColor = m_DefaultMaterial.GetColor(BaseColor);
            ApplyColor(defaultColor);
            m_Fcp.color = defaultColor;
            PlayerPrefs.DeleteKey(SaveKey + "_R");
            PlayerPrefs.DeleteKey(SaveKey + "_G");
            PlayerPrefs.DeleteKey(SaveKey + "_B");
            PlayerPrefs.DeleteKey(SaveKey + "_A");
        }
    }
    private void ApplyColor(Color color)
    {
        if (m_RuntimeMaterial != null && m_RuntimeMaterial.HasProperty(BaseColor)) m_RuntimeMaterial.SetColor(BaseColor, color);
    }
    private void SaveColor(Color color)
    {
        PlayerPrefs.SetFloat(SaveKey + "_R", color.r);
        PlayerPrefs.SetFloat(SaveKey + "_G", color.g);
        PlayerPrefs.SetFloat(SaveKey + "_B", color.b);
        PlayerPrefs.SetFloat(SaveKey + "_A", color.a);
        PlayerPrefs.Save();
    }
    private Color LoadColor()
    {
        if (PlayerPrefs.HasKey(SaveKey + "_R"))
        {
            return new Color(PlayerPrefs.GetFloat(SaveKey + "_R"),PlayerPrefs.GetFloat(SaveKey + "_G"),PlayerPrefs.GetFloat(SaveKey + "_B"),PlayerPrefs.GetFloat(SaveKey + "_A"));
        }
        if (m_DefaultMaterial != null && m_DefaultMaterial.HasProperty(BaseColor)) return m_DefaultMaterial.GetColor(BaseColor);
        return Color.white;
    }
}
