using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Dialouge.instance.deleteDialouge();
        

    }

    /// <summary>
    /// loads the current scene
    /// </summary>
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
