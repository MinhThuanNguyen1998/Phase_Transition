using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MainScene : SingletonMain<MainScene>
{
    [SerializeField] private List<View> m_ListViews = new List<View>();
    private Stack<ViewID> m_ViewHistory = new Stack<ViewID>();
    private GameObject m_CurrentView;
    private void Start() => LoadView(ViewID.Home);
    public void LoadView(ViewID viewID)
    {
        if (m_CurrentView != null) m_ViewHistory.Push(m_ListViews.Find(v => v.ViewObject == m_CurrentView).ViewID);
        ActivateView(viewID);
    }
    public void BackView()
    {
        if (m_ViewHistory.Count == 0) return;
        ViewID previousView = m_ViewHistory.Pop();
        ActivateView(previousView);
    }
    private void ActivateView(ViewID viewID)
    {
        foreach (View view in m_ListViews)
        {
            bool isActive = view.ViewID == viewID;
            if (view.ViewObject != null) view.ViewObject.SetActive(isActive);
            if (isActive) m_CurrentView = view.ViewObject;
        }
    }
    public void QuitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
