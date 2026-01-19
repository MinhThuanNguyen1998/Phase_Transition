using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CustomButton : Button
{
    private const float _ClickScale = 0.9f;        // 👈 Thu nhỏ nhẹ khi click
    private const float _TweenDuration = 0.08f;    // 👈 Thời gian tween ngắn để cảm giác mượt hơn

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        // 🔊 Phát âm thanh click
        //AudioManager.Instance?.PlaySFXButtonClick();

    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        // Có thể thêm hiệu ứng khác nếu muốn
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }
}
