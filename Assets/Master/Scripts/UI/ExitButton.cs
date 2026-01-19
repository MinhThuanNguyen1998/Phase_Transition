using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        MainManager.Instance.LoadMenu();
    }
}
