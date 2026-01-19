using UnityEngine;

[CreateAssetMenu(fileName = "DragOjectConfig", menuName = "SoftWareConfig")]
public class DragOjectConfig : ScriptableObject
{
    [Header("Drag Settings")]
    public float smoothSpeed = 5f;

    [Header("Mouse Clamp Offset (pixel margins)")]
    public Vector2 screenEdgeOffset = new Vector2(50f, 50f); // ví dụ: cách mép 50px

    [Header("Audio Settings")]
    public AudioClip dragAudioClip;
}
