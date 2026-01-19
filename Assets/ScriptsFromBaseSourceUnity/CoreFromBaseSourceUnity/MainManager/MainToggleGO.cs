using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class MainToggleGO : MonoBehaviour
{
    public GameObject MenuGO;
    public bool toggleActiveMenu;
    public GameObject ExpGO;
    public bool toggleActiveExp;
    public GameObject TutorialGO;
    public bool toggleActiveTutorial;

#if UNITY_EDITOR
    private void Update()
    {
        // Chỉ chạy trong Editor, không ảnh hưởng runtime
        if (!Application.isPlaying && MenuGO != null)
        {
            MenuGO.SetActive(toggleActiveMenu);
        }
        if (!Application.isPlaying && ExpGO != null)
        {
            ExpGO.SetActive(toggleActiveExp);
        }
        if (!Application.isPlaying && TutorialGO != null)
        {
            TutorialGO.SetActive(toggleActiveTutorial);
        }
    }
#endif
}
