using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class LabMode : MonoBehaviour
{
    public static event Action<int> OnGotoState;
    [SerializeField] private TMP_Dropdown m_DropDownStateOfMatter;
   
    private void OnEnable() 
    {
        m_DropDownStateOfMatter.onValueChanged.AddListener(OnStateChanged);  
    }
    private void OnDisable() 
    {
        m_DropDownStateOfMatter.onValueChanged.RemoveListener(OnStateChanged);
    } 
    private void OnStateChanged(int index)
    {
        string selectedState = m_DropDownStateOfMatter.options[index].text;
        
    }
}
