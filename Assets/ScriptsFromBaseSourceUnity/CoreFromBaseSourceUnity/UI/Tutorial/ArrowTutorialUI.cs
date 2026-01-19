using UnityEngine;
using DG.Tweening;

public class ArrowTutorialUI : MonoBehaviour
{
    [SerializeField] private RectTransform panelPosition;
    public RectTransform PanelPosition => panelPosition;

    [SerializeField] private RectTransform rectTransform;
    private Vector2 animationPosition;
    private Tween moveTween;

    [Header("Animation Settings")]
    [SerializeField] private float moveDistance = 30f;
    [SerializeField] private float moveDuration = 1f;

    public void UpdateStartPosition(Vector2 animationPosition)
    {
        this.animationPosition = animationPosition;
        rectTransform.anchoredPosition = animationPosition;

        if (moveTween != null && moveTween.IsActive())
            moveTween.Kill();

        float zRot = Mathf.Abs(rectTransform.eulerAngles.z % 180f);
        // 🔁 Đảo điều kiện: nếu góc 90° (đứng dọc) thì di chuyển X, nếu 0° (nằm ngang) thì di chuyển Y
        bool isMoveOnX = (zRot >= 45f && zRot <= 135f);

        Vector2 targetPos = animationPosition;
        if (isMoveOnX)
            targetPos.x += moveDistance;
        else
            targetPos.y += moveDistance;

        moveTween = rectTransform.DOAnchorPos(targetPos, moveDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        if (moveTween != null && moveTween.IsActive())
            moveTween.Kill();
    }
}
