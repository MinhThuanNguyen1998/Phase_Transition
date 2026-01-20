using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;

public class StateChangeController : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_StateList;
    [SerializeField] private GameObject m_MoleculeWarning;

    private void OnEnable() 
    {
        WarnIfMoleculesFull(false);
        StoveUIController.OnStateChanged += ChangeState;
    }
    private void OnDisable() => StoveUIController.OnStateChanged -= ChangeState;
   
    public void ChangeState()
    {
        ApplyStateFromConfig();
        WarnIfMoleculesFull(false);
    }
    public void WarnIfMoleculesFull(bool isWarning)
    {
        //m_MoleculeWarning.SetActive(isWarning);
        //if (isWarning) SetAllStates(false);
        //else ApplyStateFromConfig();
    }
    private void SetActiveStateByIndex(int index)
    {
        for (int i = 0; i < m_StateList.Count; i++)
        {
            m_StateList[i].SetActive(i == index);
        }
    }
    private void SetAllStates(bool isActive)
    {
        foreach (var state in m_StateList)
        {
            state.SetActive(isActive);
        }
    }
    private void ApplyStateFromConfig()
    {
        int index = TemperatureModel.GetStateIndex();
        SetActiveStateByIndex(index);
    }
}
