using System;
using Michsky.MUIP;
using TMPro;
using UnityEngine;

public class VoiceDropDownList : MonoBehaviour
{
    [SerializeField] private CustomDropdown m_DropDownVoice;
    public static int CurrentVoiceIndex { get; set; }
    private void OnEnable()
    {
        if (m_DropDownVoice != null)
            m_DropDownVoice.onValueChanged.AddListener(OnChangeVoice);
        SyncUIDropDownList();
    }
    private void Start()
    {
        if (m_DropDownVoice != null)
            CurrentVoiceIndex = m_DropDownVoice.index;
    }
    private void OnDestroy()
    {
        if (m_DropDownVoice != null)
            m_DropDownVoice.onValueChanged.RemoveListener(OnChangeVoice);
    }
    private void OnChangeVoice(int index)
    {
        CurrentVoiceIndex = index;
    }
    private void SyncUIDropDownList()
    {
        m_DropDownVoice.selectedItemIndex = CurrentVoiceIndex;
        m_DropDownVoice.SetDropdownIndex(m_DropDownVoice.selectedItemIndex);
    }
}
