using UnityEngine;

[CreateAssetMenu(fileName = "OutlineSettings", menuName = "Settings/Outline Settings")]
public class OutlineSettingsSO : ScriptableObject
{
    [Header("Outline Settings for Movable Object")]
    public Color movableColor = Color.green;
    [Range(0f, 10f)] public float movableWidth = 3f;
    public Outline.Mode movableMode = Outline.Mode.OutlineAll;

    [Header("Outline Settings for Static Object")]
    public Color staticColor = Color.yellow;
    [Range(0f, 10f)] public float staticWidth = 2f;
    public Outline.Mode staticMode = Outline.Mode.OutlineAll;
}
