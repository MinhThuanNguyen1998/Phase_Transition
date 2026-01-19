using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum PopupType
{
    Notification,
    Introduction  
}
public class PopUpManager : SingletonMain<PopUpManager>
{
    [SerializeField] private PopUp m_NotificationPopupPrefab;
    [SerializeField] private PopUp m_IntrodutionPopupPrefab;
    private Dictionary<PopupType, Queue<PopUp>> m_PopupPool = new Dictionary<PopupType, Queue<PopUp>>();
    private void Awake()
    {
        m_PopupPool[PopupType.Notification] = new Queue<PopUp>();
        m_PopupPool[PopupType.Introduction] = new Queue<PopUp>();
    }
    private PopUp GetPopup(PopupType popupType)
    {
        PopUp popup = null;
        var pool = m_PopupPool[popupType];
        if (pool.Count > 0)
        {
            popup = pool.Dequeue();
            popup.gameObject.SetActive(true);
        }
        else
        {
            PopUp prefab = GetPrefabByType(popupType);
            if (prefab != null) popup = Instantiate(prefab);
            else Debug.LogError($"[PopUpManager] Prefab not assigned for {popupType}");
           
        }
        return popup;
    }
    private PopUp GetPrefabByType(PopupType type)
    {
        switch (type)
        {
            case PopupType.Notification: return m_NotificationPopupPrefab;
            case PopupType.Introduction: return m_IntrodutionPopupPrefab;
            default: return null;
        }
    }
    public void ShowPopUp(PopupType popupType, string content, string okLabel, string cancelLabel,
                           Action onOk = null, Action onCancel = null)
    {
        PopUp popup = GetPopup(popupType);
        if (popup == null) return;
        popup.SetContent(content, okLabel, cancelLabel,
            () =>
            {
                onOk?.Invoke();
                ReturnToPool(popupType, popup);
            },
            () =>
            {
                onCancel?.Invoke();
                ReturnToPool(popupType, popup);
            });
    }

    private void ReturnToPool(PopupType popupType, PopUp popup)
    {
        popup.gameObject.SetActive(false);
        m_PopupPool[popupType].Enqueue(popup);
    }
}
