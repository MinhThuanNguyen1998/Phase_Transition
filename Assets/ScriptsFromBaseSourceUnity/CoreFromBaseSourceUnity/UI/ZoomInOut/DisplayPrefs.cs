using UnityEngine;

public static class DisplayPrefs
{
    private const string KEY_FULLSCREEN = "Display_IsFullScreen";

    public static bool IsFullScreen
    {
        get => PlayerPrefs.GetInt(KEY_FULLSCREEN, 1) == 1; // mặc định: full screen
        set
        {
            PlayerPrefs.SetInt(KEY_FULLSCREEN, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}
