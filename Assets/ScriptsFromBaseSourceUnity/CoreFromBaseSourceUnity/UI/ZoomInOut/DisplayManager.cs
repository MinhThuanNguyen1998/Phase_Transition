using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DisplayManager : MonoBehaviour
{
 
    [Header("Button Icons")]
    public Sprite fullScreenIcon;
    public Sprite miniScreenIcon;
    public Image ScreenButtonImage;
    void Start()
    {
        UpdateButtonIconSafe();
    }

    public void ToggleFullScreen()
    {
        MainManager.Instance.ToggleDisplayMode();

        UpdateButtonIconSafe();

        EventSystem.current.SetSelectedGameObject(null);
    }

    private void UpdateButtonIconSafe()
    {
        // Nếu thiếu icon hoặc hình ảnh nút, chỉ đổi chế độ hiển thị mà không đụng tới UI
        if (ScreenButtonImage == null || fullScreenIcon == null || miniScreenIcon == null)
        {
            //Debug.LogWarning("[DisplayManager] Missing screen icons or button image — switching display only.");
            return;
        }

        ScreenButtonImage.sprite = !MainManager.Instance.IsFullScreen ? miniScreenIcon : fullScreenIcon;
    }


}
