using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class FocusHelper
{
    public static void SetupArrow(ArrowTutorialUI arrow,GameObject target,Camera mainCamera,Canvas mainCanvas,float arrowOffset,List<GameObject> listObjectOnTop,List<GameObject> listObjectOnDown,List<GameObject> listObjectOnLeft,List<GameObject> listObjectOnRight,MonoBehaviour coroutineOwner
     )
    {
        if (arrow == null || target == null || mainCamera == null || mainCanvas == null)
            return;

        Vector2 screenPos = TutorialUIHelper.GetScreenPosition(target, mainCamera);
        Vector2 arrowScreenPos = TutorialUIHelper.GetArrowPositionByList(screenPos,target,mainCamera,listObjectOnTop,listObjectOnDown,listObjectOnLeft,listObjectOnRight,arrowOffset,out float rotationZ);

        // --- Convert screen → local canvas position ---
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvas.transform as RectTransform,arrowScreenPos,null,out Vector2 localArrowPos);
        // --- Apply to arrow ---
        RectTransform arrowRect = arrow.GetComponent<RectTransform>();
        arrowRect.anchoredPosition = localArrowPos;
        arrowRect.localRotation = Quaternion.Euler(0, 0, rotationZ);
        arrow.gameObject.SetActive(true);
        arrow.UpdateStartPosition(localArrowPos);
        // Chờ 1 frame để layout ổn định (nếu cần)
        coroutineOwner.StartCoroutine(DelayedArrowTweenUpdate());
    }
    private static IEnumerator DelayedArrowTweenUpdate()
    {
        yield return null;
    }
    public static void UpdateImage(Image focusImage,GameObject target,Camera mainCamera,Canvas mainCanvas,float focusPadding = 1.3f)
    {
        if (focusImage == null || target == null || mainCamera == null || mainCanvas == null) return;
        Vector2 screenPos = TutorialUIHelper.GetScreenPosition(target, mainCamera);
        Vector2 size = TutorialUIHelper.GetObjectScreenSize(target, mainCamera);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvas.transform as RectTransform,screenPos,null,out Vector2 localPos);
        RectTransform focusRect = focusImage.GetComponent<RectTransform>();
        focusRect.anchoredPosition = localPos;
        focusRect.sizeDelta = size * focusPadding;
        focusImage.gameObject.SetActive(true);
    }
    public static void PositionInfoPanel(GameObject infoPanel,ArrowTutorialUI arrow,Canvas mainCanvas)
    {
        if (infoPanel == null || arrow == null || mainCanvas == null) return;
        RectTransform panelRect = infoPanel.GetComponent<RectTransform>();
        RectTransform canvasRect = mainCanvas.GetComponent<RectTransform>();

        // Gán vị trí panel theo mũi tên
        Vector3 panelWorldPos = arrow.PanelPosition.position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,RectTransformUtility.WorldToScreenPoint(null, panelWorldPos),null,out Vector2 anchoredPos);
        // Kích thước panel + canvas
        Vector2 panelSize = panelRect.rect.size;
        Vector2 canvasSize = canvasRect.rect.size;
        float halfW = panelSize.x / 2f, halfH = panelSize.y / 2f;

        // Giới hạn vị trí để không tràn khỏi màn hình
        anchoredPos.x = Mathf.Clamp(anchoredPos.x, -canvasSize.x / 2f + halfW, canvasSize.x / 2f - halfW);
        anchoredPos.y = Mathf.Clamp(anchoredPos.y, -canvasSize.y / 2f + halfH, canvasSize.y / 2f - halfH);
        // Gán lại
        panelRect.anchoredPosition = anchoredPos;
    }
}