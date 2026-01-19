using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class TutorialUIHelper
{
    /// Lấy vị trí màn hình của object (UI hoặc 3D)
    public static Vector2 GetScreenPosition(GameObject target, Camera mainCamera)
    {
        if (target == null) return Vector2.zero;
        // UI Object
        if (target.TryGetComponent(out RectTransform rectTransform) &&
            target.layer == LayerMask.NameToLayer("UI"))
        {
            return rectTransform.position;
        }
        // 3D Object
        return mainCamera.WorldToScreenPoint(target.transform.position);
    }

    /// Lấy kích thước của object (UI hoặc 3D) trên màn hình
    public static Vector2 GetObjectScreenSize(GameObject target, Camera mainCamera)
    {
        // --- UI ---
        if (target.TryGetComponent(out RectTransform rect))
            return rect.rect.size;
        // --- 3D Object ---
        Renderer renderer = target.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            Bounds bounds = renderer.bounds;
            Vector3 min = bounds.min;
            Vector3 max = bounds.max;
            Vector3 screenMin = mainCamera.WorldToScreenPoint(min);
            Vector3 screenMax = mainCamera.WorldToScreenPoint(max);
            float width = Mathf.Abs(screenMax.x - screenMin.x);
            float height = Mathf.Abs(screenMax.y - screenMin.y);
            return new Vector2(width, height);
        }
        // fallback
        return new Vector2(100f, 100f);
    }
    /// Tính vị trí và hướng của mũi tên dựa trên vị trí object
    public static Vector2 GetArrowPositionByList(
        Vector2 screenPos,
        GameObject target,
        Camera mainCamera,
        List<GameObject> listObjectOnTop,
        List<GameObject> listObjectOnDown,
        List<GameObject> listObjectOnLeft,
        List<GameObject> listObjectOnRight,
        float arrowOffset,
        out float rotationZ)
    {
        Vector2 size = GetObjectScreenSize(target, mainCamera);
        Vector2 halfSize = size / 2f;

        Vector2 arrowPos = screenPos;
        rotationZ = 0f;
        if (listObjectOnTop.Contains(target))
        {
            arrowPos = new Vector2(screenPos.x, screenPos.y - (halfSize.y + arrowOffset));
            rotationZ = 180f;
        }
        else if (listObjectOnDown.Contains(target))
        {
            arrowPos = new Vector2(screenPos.x, screenPos.y + (halfSize.y + arrowOffset));
            rotationZ = 0f;
        }
        else if (listObjectOnLeft.Contains(target))
        {
            arrowPos = new Vector2(screenPos.x + (halfSize.x + arrowOffset), screenPos.y);
            rotationZ = -90f;
        }
        else if (listObjectOnRight.Contains(target))
        {
            arrowPos = new Vector2(screenPos.x - (halfSize.x + arrowOffset), screenPos.y);
            rotationZ = 90f;
        }
        arrowPos.x = Mathf.Clamp(arrowPos.x, 50f, Screen.width - 50f);
        arrowPos.y = Mathf.Clamp(arrowPos.y, 50f, Screen.height - 50f);
        return arrowPos;
    }
    public static List<GameObject> SortByScreenPosition(List<GameObject> list,Camera mainCamera,bool isVertical,bool descending)
    {
        if (list == null || list.Count == 0) return list;
        if (isVertical)
            return descending
                ? list.OrderByDescending(o => GetScreenPosition(o, mainCamera).y).ToList()
                : list.OrderBy(o => GetScreenPosition(o, mainCamera).y).ToList();
        else
            return descending
                ? list.OrderByDescending(o => GetScreenPosition(o, mainCamera).x).ToList()
                : list.OrderBy(o => GetScreenPosition(o, mainCamera).x).ToList();
    }
}
