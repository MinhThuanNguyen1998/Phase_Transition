using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HintButtonUI : MonoBehaviour
{
    [Header("Cấu hình hiệu ứng UI")]
    public float moveDistance = 20f;    // Khoảng di chuyển theo trục Y (px)
    public float duration = 1.5f;       // Thời gian hoàn thành 1 lượt lên hoặc xuống
    public Ease easeType = Ease.InOutSine; // Kiểu easing (mượt mà)
    public bool randomStartDelay = true;   // Random delay để không đồng bộ

    private RectTransform rectTransform;
    private Vector2 startPos;
    private Tween floatTween;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        StartFloating();
    }

    void StartFloating()
    {
        // float delay = randomStartDelay ? Random.Range(0f, 1f) : 0f;
        if (rectTransform != null)
        {
            rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = startPos;
        }

        floatTween = rectTransform.DOAnchorPosY(startPos.y + moveDistance, duration)
            .SetEase(easeType)
            .SetLoops(-1, LoopType.Yoyo);
        // .SetDelay(delay);
    }

    void OnDisable()
    {
        floatTween?.Kill();
    }

    void OnDestroy()
    {
        floatTween?.Kill();
    }
    void OnEnable()
    {
        if (floatTween == null || !floatTween.IsActive())
        {
            StartFloating();
        }
    }
}
