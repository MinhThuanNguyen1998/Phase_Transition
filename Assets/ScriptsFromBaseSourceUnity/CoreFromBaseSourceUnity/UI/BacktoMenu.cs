using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacktoMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        MainManager.Instance.LoadMenu();
    }
}
