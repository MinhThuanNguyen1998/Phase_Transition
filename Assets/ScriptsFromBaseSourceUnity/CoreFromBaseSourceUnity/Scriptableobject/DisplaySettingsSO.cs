using UnityEngine;

[CreateAssetMenu(fileName = "DisplaySettings", menuName = "Settings/Display Settings", order = 0)]
public class DisplaySettingsSO : ScriptableObject
{
    [Tooltip("Đang ở chế độ toàn màn hình hay không")]
    public bool isFullScreen = true;

}
