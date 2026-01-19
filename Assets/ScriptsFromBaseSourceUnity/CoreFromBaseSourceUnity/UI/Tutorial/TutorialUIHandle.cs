using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUIHandle : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private List<TutorialSO> m_tutorialList;
    [SerializeField] private TutorialSO m_tutorialData;
    [Header("Reference")]
    [SerializeField] private TutorialUI m_tutorialUI;
    [SerializeField] private TextMeshProUGUI m_buttonNameText;
    [SerializeField] private TextMeshProUGUI m_buttonContenText;
    [SerializeField] private GameObject m_ButtonPrevious;
    [SerializeField] private Canvas canvas;

    private void OnEnable()
    {
        SetTutorialData(VoiceDropDownList.CurrentVoiceIndex);
        SetUp();
        ShowStep(0);
        ActiveButtonPrev(false);
    }
    private void SetTutorialData(int index)
    {
        if (index >= 0 && index < m_tutorialList.Count)
        {
            m_tutorialData = m_tutorialList[index];
        }
        else Debug.LogWarning("[TutorialUIHandle] Invalid tutorial index!");
    }
    private void SetUp()
    {
        canvas.worldCamera = Camera.main;
    }
    public void NextStep()
    {
        int index = m_tutorialUI.ShowNextStep();
        //ket thuc tutoral
        ShowStep(index);
        ActiveButtonPrev(true);
        Debug.Log("index:" + index);
    }
    public void Skip()
    {
        MainManager.Instance.LoadExp();
    }
    public void PreViousStep()
    {
        int index = m_tutorialUI.ShowPreViousStep();
        ShowStep(index);
        Debug.Log("index:" + index);
    }
    private void ShowStep(int index)
    {

        if (index < 0 || index >= m_tutorialData.Data.Count)
        {
            MainManager.Instance.LoadExp();
            return;
        }
        if (index == 0) ActiveButtonPrev(false);
        var data = m_tutorialData.Data[index];
        m_buttonNameText.text = data.buttonName;
        m_buttonContenText.text = data.buttonContent;
        AudioMainManager.Instance.PlayAudioIntroduction(data.buttonAudio);
    }
    public void TutorialEnd()
    {
        //AudioManager.Instance.StopAll();
        MainManager.Instance.LoadExp();
    }

    private void ActiveButtonPrev(bool isActive)
    {
        m_ButtonPrevious.SetActive(isActive);
    }
}
