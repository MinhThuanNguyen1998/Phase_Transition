using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public void CallReloadScene()
    {
        OpenUI.CloseAllUI();
        MainManager.Instance.LoadExp();

    }
}
