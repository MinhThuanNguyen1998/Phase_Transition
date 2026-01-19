using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] private Button m_OkButton;
    [SerializeField] private Button m_CancelButton;

    [SerializeField] private TextMeshProUGUI m_TextContent;
    [SerializeField] private TextMeshProUGUI m_TextButtonOK;
    [SerializeField] private TextMeshProUGUI m_TextButtonCancel;

    private Action m_OnOkClick;
    private Action m_OnCancelClick;

    public void SetContent(string content, string okLabel, string cancelLabel, Action onOK, Action onCancel)
    {
        m_TextContent.text = content;
        m_TextButtonOK.text = okLabel;
        m_TextButtonCancel.text = cancelLabel;

        m_OnOkClick = onOK;
        m_OnCancelClick = onCancel;

        m_OkButton.onClick.RemoveAllListeners();
        m_CancelButton.onClick.RemoveAllListeners();

        m_OkButton.onClick.AddListener(() =>
        {
            m_OnOkClick?.Invoke();
            AudioMainManager.Instance.PlayOnShot(SoundType.Button);
            Close();
        });
        m_CancelButton.onClick.AddListener(() =>
        {
            m_OnCancelClick?.Invoke();
            Close();
        });
    }
    private void Close()
    {
        gameObject.SetActive(false);
        m_OnOkClick = null;
        m_OnCancelClick = null;
    }
}
