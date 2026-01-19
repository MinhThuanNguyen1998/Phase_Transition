using UnityEngine;

[DisallowMultipleComponent]
public class OutlineHoverEffect: MonoBehaviour
{
    [SerializeField] private MyOutLine m_MyOutline;

    [Header("Object Type")]
    [Tooltip("Nếu bật, object này là loại có thể di chuyển.")]
    [SerializeField] private bool isMovable = false;

    // Shared settings cho toàn project
    private static OutlineSettingsSO sharedSettings;

    private void Awake()
    {
        // Lấy reference Outline
        if (m_MyOutline == null)
            m_MyOutline = GetComponent<MyOutLine>();

        // Load ScriptableObject chỉ 1 lần
        if (sharedSettings == null)
        {
            sharedSettings = Resources.Load<OutlineSettingsSO>("Settings/OutlineSettings");
            if (sharedSettings == null)
            {
                Debug.LogWarning("⚠️ OutlineSettings.asset not found in Resources/Settings/");
                return;
            }
        }

        if (m_MyOutline == null)
        {
            Debug.LogWarning($"⚠️ Outline component missing on {name}");
            return;
        }

        // Áp dụng cài đặt Outline tương ứng
        ApplyOutlineSettings();

        // Tắt mặc định để tiết kiệm performance
        m_MyOutline.enabled = false;
    }

    private void ApplyOutlineSettings()
    {
        if (isMovable)
        {
            m_MyOutline.OutlineColor = sharedSettings.movableColor;
            m_MyOutline.OutlineWidth = sharedSettings.movableWidth;

        }
        else
        {
            m_MyOutline.OutlineColor = sharedSettings.staticColor;
            m_MyOutline.OutlineWidth = sharedSettings.staticWidth;
        }
           
    }
    private void OnMouseEnter()
    {
        m_MyOutline.enabled = true;
    }

    private void OnMouseExit()
    {
        m_MyOutline.enabled = false;
    }
}


