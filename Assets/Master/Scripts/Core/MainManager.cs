using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StartMode
{
    Menu,
    ExpProcess,
    Tutorial
}
public class MainManager : Singleton<MainManager>
{
    [Header("Prefabs / Scenes")]
    [SerializeField] private GameObject m_expGO;
    [SerializeField] private GameObject m_tutorialGO;
    [SerializeField] private GameObject m_menuGO;

    [Header("Prefabs / Scenes")]
    [SerializeField] private GameObject m_GroupVoice;
    [Header("Transition Setting")]
    [SerializeField] private TransitionSettings m_transitionSettings;

    [Header("Start Mode")]
    [SerializeField] private StartMode m_startMode = StartMode.Menu;

    // Danh sách prefab cần preload
    private readonly List<GameObject> preloadList = new List<GameObject>();
    private bool isFirstLoad = true;

    // 🔹 Lưu trạng thái fullscreen qua PlayerPrefs
    private const string PREF_FULLSCREEN_KEY = "Display_IsFullScreen";

    public bool IsFullScreen => PlayerPrefs.GetInt(PREF_FULLSCREEN_KEY, 1) == 1;

    private void Start()
    {
        ApplyDisplayMode(); // Áp dụng chế độ hiển thị đã lưu
        // Gom tất cả prefab cần preload
        preloadList.Add(m_expGO);
        preloadList.Add(m_tutorialGO);
        ActiveGameObjectPrefabView(true);

        // Bắt đầu preload
        StartCoroutine(PreloadPrefabs());

    }

    private void ActiveGameObjectPrefabView(bool isActive)
    {
        m_expGO.SetActive(!isActive);
        m_menuGO.SetActive(isActive);
        m_GroupVoice.SetActive(isActive);
        m_tutorialGO.SetActive(!isActive);
    }
    private IEnumerator PreloadPrefabs()
    {
        foreach (var prefab in preloadList)
        {
            if (prefab == null) continue;
            prefab.SetActive(false);
            yield return null;
        }

        Debug.Log("[MainManager] Prefabs preloaded.");

        // Sau khi preload xong, nếu không phải chế độ Menu thì load prefab tương ứng luôn
        if (m_startMode == StartMode.ExpProcess)
        {
            LoadNewScene(m_expGO);
        }
        else if (m_startMode == StartMode.Tutorial)
        {
            LoadNewScene(m_tutorialGO);
        }
    }
    private void LoadNewScene(GameObject prefab)
    {
        if (TransitionManager.Instance().IsRunningTransition) return;

        GameObject _tmp = null;
        if (isFirstLoad)
        {
            _tmp = m_menuGO;
            isFirstLoad = false;
        }
        TransitionManager.Instance().Transition(prefab, transform, m_transitionSettings, 0.2f, 0.5f, _tmp);
        bool shouldShowGroupVoice = prefab == m_menuGO;
        StartCoroutine(ToggleGroupVoiceWithDelay(shouldShowGroupVoice, 1f));
    }
    private IEnumerator ToggleGroupVoiceWithDelay(bool active, float delay)
    {
        yield return new WaitForSeconds(delay);
        m_GroupVoice.SetActive(active);
    }
    public void LoadExp() => LoadNewScene(m_expGO);
    public void LoadTutorial() => LoadNewScene(m_tutorialGO);
    public void LoadMenu() => LoadNewScene(m_menuGO);

    public void Exit()
    {
        Debug.Log("[MainManager] Exit game.");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void ApplyDisplayMode()
    {
        bool isFull = IsFullScreen;

        if (isFull)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, true);
            Debug.Log("[MainManager] Applied Fullscreen mode.");
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.SetResolution(1280, 720, false);
            Debug.Log("[MainManager] Applied Windowed mode.");
        }

        if (Camera.main != null)
            Camera.main.ResetAspect();
    }
    public void ToggleDisplayMode()
    {
        bool isFull = IsFullScreen;
        bool newState = !isFull;
        // 🔁 Lưu trạng thái mới
        PlayerPrefs.SetInt(PREF_FULLSCREEN_KEY, newState ? 1 : 0);
        PlayerPrefs.Save();
        // Áp dụng thay đổi
        ApplyDisplayMode();

        Debug.Log($"[MainManager] Toggled display mode → {(newState ? "Fullscreen" : "Windowed")}");
    }
}
