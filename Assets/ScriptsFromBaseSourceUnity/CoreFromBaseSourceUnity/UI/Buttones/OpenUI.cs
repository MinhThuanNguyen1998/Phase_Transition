using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Settings")]
    [SerializeField] private RectTransform UI;
    [SerializeField] private bool m_IsUpdateButtonSprite = true;

    [Header("Button References")]
    [SerializeField] private Image targetImage;

    private SpriteButtonSO spriteButtonSO;

    private static OpenUI activeInstance;
    private bool isHovered;
    private bool isOpen;

    private void Awake()
    {
        // 🔹 Nếu chưa có Image thì lấy trên object
        if (targetImage == null)
            targetImage = GetComponent<Image>();

        // 🔹 Tự động load SpriteButtonSO nếu chưa gán
        if (spriteButtonSO == null)
        {
            spriteButtonSO = Resources.Load<SpriteButtonSO>("Settings/SpriteButtonSO");
            if (spriteButtonSO == null)
                Debug.LogWarning("⚠️ Không tìm thấy SpriteButtonSO trong thư mục Resources/Settings!");
        }

        // 🔹 Tắt UI nếu đang bật mà không phải instance chính
        if (UI != null && UI.gameObject.activeSelf && this != activeInstance)
            UI.gameObject.SetActive(false);

        UpdateButtonSprite();
    }
    private void OnEnable() => ResetSprite();

    private void OnDisable() => ResetSprite();

    public void OnClick()
    {
        //AudioManager.Instance?.PlaySFXInMap(SFXType.ButtonClick);

        // 🔹 Nếu có UI khác đang mở → đóng nó
        if (activeInstance != null && activeInstance != this)
        {
            activeInstance.SetUIState(false);
            activeInstance = null;
        }

        isOpen = !UI.gameObject.activeSelf;
        SetUIState(isOpen);

        // 🔹 Cập nhật instance đang mở
        activeInstance = isOpen ? this : null;
    }

    private void SetUIState(bool open)
    {
        if (UI != null)
            UI.gameObject.SetActive(open);

        DragObject.GlobalDisableDrag = open;
        isOpen = open;
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        if (targetImage == null || !m_IsUpdateButtonSprite || spriteButtonSO == null) return;

        if (isOpen)
            targetImage.sprite = spriteButtonSO.pressedSprite;
        else if (isHovered)
            targetImage.sprite = spriteButtonSO.hoverSprite;
        else
            targetImage.sprite = spriteButtonSO.normalSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        if (!isOpen) UpdateButtonSprite();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        if (!isOpen) UpdateButtonSprite();
    }

    public static void CloseAllUI()
    {
        if (activeInstance != null)
        {
            activeInstance.SetUIState(false);
            activeInstance = null;
        }
    }

    private void ResetSprite() => targetImage.sprite = spriteButtonSO.normalSprite;
}




