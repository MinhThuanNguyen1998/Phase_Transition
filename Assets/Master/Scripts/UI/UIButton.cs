using System;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] UIButtonType m_ButtonType;
    public static event Action<UIButtonType> OnButtonClicked;

    public void OnClick()
    {
        //Debug.Log("Click");
        OnButtonClicked?.Invoke(m_ButtonType);
    }
}
