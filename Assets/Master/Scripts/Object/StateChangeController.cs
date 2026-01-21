using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
public class StateChangeController : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_StateList;
    [SerializeField] private GameObject m_MoleculeWarning;
    [SerializeField] private CameraShakeEffect m_ShakeEffect;
    private bool m_IsWarningActive;

    private void OnEnable() 
    {
        WarnIfMoleculesFull(false);
        StoveUIController.OnStateChangedUI += ChangeState;
        BottomFlaskTrigger.OnStateChangedUI += ChangeState;
    }
    private void OnDisable() 
    {
        StoveUIController.OnStateChangedUI -= ChangeState;
        BottomFlaskTrigger.OnStateChangedUI -= ChangeState;
    }
    public void ChangeState()
    {
        if (m_IsWarningActive) return;
        ApplyStateFromConfig();
        WarnIfMoleculesFull(false);
    }
    public void WarnIfMoleculesFull(bool isWarning)
    {
        m_IsWarningActive = isWarning;
        m_MoleculeWarning.SetActive(isWarning);

        if (isWarning)
        {
            SetAllStates(false);
            m_ShakeEffect.Shake();
            //MainManager.Instance.LoadExp();
        }
        else ApplyStateFromConfig();
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
