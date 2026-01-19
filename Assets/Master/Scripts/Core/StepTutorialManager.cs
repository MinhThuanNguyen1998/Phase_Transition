using System.Collections;
using System.Collections.Generic;
// using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StepTutorialManager : Singleton<StepTutorialManager>
{
    [SerializeField] private GameObject m_StepTutorialPanel;
    [SerializeField] private List<GameObject> m_ListSteps = new();
    [SerializeField] private float m_DelayShowTutorialIcon = 5f;
    [SerializeField] private List<VideoPlayer> m_VideoPlayers = new();
    [SerializeField] private GameObject m_TutorialIcon;
    [SerializeField] private GameObject m_HideObj;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite pressedSprite;
    private int _currentState = 0;
    private Coroutine startTimerCoroutine;

    private float _DelayShowTutorialIcon = 0f;
    private bool IsFirstTimeShowTutorial = true;

    private void Start()
    {
        m_HideObj.gameObject.SetActive(false);
        ChangeState(0);
    }
    public void GotoState(int state)
    {
        if (state == _currentState) return;
        ChangeState(state);
    }
    private void ChangeState(int state)
    {
        _currentState = state;
        // UIPopupHelper.Popdown(m_TutorialIcon);
        m_TutorialIcon.SetActive(false);

        if (startTimerCoroutine != null)
        {
            StopCoroutine(startTimerCoroutine);
        }

        startTimerCoroutine = StartCoroutine(StartTimer());
    }
    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(_DelayShowTutorialIcon);
        // UIPopupHelper.Popup(m_TutorialIcon);
        m_TutorialIcon.SetActive(true);

    }
    public void ShowCurrentStateTutorial()
    {
        Debug.Log("Show current state");
        //UIPopupHelper.Popup(m_StepTutorialPanel);
        m_VideoPlayers[_currentState].Stop();
        m_VideoPlayers[_currentState].Play();
        for (int i = 0; i < m_ListSteps.Count; i++)
        {
            m_ListSteps[i].SetActive(i == _currentState);
        }
    }
    public void HideCurrentStateTutorial()
    {
        //UIPopupHelper.Popdown(m_StepTutorialPanel);
    }
    public void ToggleHintMode(Image image)
    {
        m_HideObj.SetActive(!m_HideObj.activeSelf);

        if (m_HideObj.activeSelf)
        {
            if(IsFirstTimeShowTutorial)
            {
                IsFirstTimeShowTutorial = false;
                _DelayShowTutorialIcon = m_DelayShowTutorialIcon;
            }
            image.sprite = pressedSprite;
        }
        else
        {
            image.sprite = normalSprite;
        }
    }
}
