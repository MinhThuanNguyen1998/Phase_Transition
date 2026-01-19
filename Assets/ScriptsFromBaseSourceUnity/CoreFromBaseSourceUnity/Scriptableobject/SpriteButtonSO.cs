using UnityEngine;

[CreateAssetMenu(fileName = "SpriteButtonSO", menuName = "Settings/Sprite Button SO")]
public class SpriteButtonSO : ScriptableObject
{
    [Header("Button Sprites")]
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public Sprite pressedSprite;
}
