using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopUIManger : MonoBehaviour
{
    public void BackToMenu()
    {
        MainManager.Instance.LoadMenu();
    }
    public void ResetEXP()
    {
        MainManager.Instance.LoadExp();
    }

}
