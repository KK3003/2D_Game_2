using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppClose : MonoBehaviour
{
    GameCtrl gamectrl;

    public void CloseApp()
    {
        
        Application.Quit();
        gamectrl.DeleteCheckpoints();
        PlayerPrefs.SetInt("Checkpoint", 0);
    }
}
