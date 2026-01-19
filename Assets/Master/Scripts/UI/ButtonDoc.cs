using System;
using System.Collections.Generic;
using Michsky.MUIP;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDoc : MonoBehaviour
{
    [SerializeField] private Image m_DefaultSoundImage;
    [SerializeField] private Image m_PauseImage;
    [SerializeField] private Button m_SoundDocButton;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private List<AudioClip> m_ListAudioClip;
    [SerializeField] private CustomDropdown m_DropDownVoice;
    [SerializeField] private ScrollRect m_ScrollRect;
    private bool isSoundOn = true;

    private void OnEnable() => ResetButtonDocState();
    private void Start()
    {
        m_SoundDocButton.onClick.AddListener(ToggleSoundIcon);
        if (m_DropDownVoice != null)m_DropDownVoice.onValueChanged.AddListener(OnChangeVoice);
        ResetButtonDocState();
        SyncUIDropDownList();
    }
    private void OnChangeVoice(int index) => VoiceDropDownList.CurrentVoiceIndex = index;
  
    private void SyncUIDropDownList()
    {
        m_DropDownVoice.selectedItemIndex = VoiceDropDownList.CurrentVoiceIndex;
        //m_DropDownVoice.SetupDropdown();
        m_DropDownVoice.SetDropdownIndex(m_DropDownVoice.selectedItemIndex);  
    }
    private void ToggleSoundIcon()
    {
        isSoundOn = !isSoundOn;
        if (!isSoundOn)
        {
            m_AudioSource.clip = m_ListAudioClip[VoiceDropDownList.CurrentVoiceIndex];
            m_AudioSource.Play();
        }
        else m_AudioSource.Pause();
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_DefaultSoundImage.gameObject.SetActive(isSoundOn);
        m_PauseImage.gameObject.SetActive(!isSoundOn);
    }
    private void ResetButtonDocState()
    {
        isSoundOn = true;
        UpdateUI();
        m_ScrollRect.verticalNormalizedPosition = 1f;
    }
}
