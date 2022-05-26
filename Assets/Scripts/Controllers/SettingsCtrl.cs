using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  provides functionality to the Social buttons like facebook, twitterurl , googleplusurl , retingURL
/// </summary>
public class SettingsCtrl : MonoBehaviour
{
    public string ratingURL;


    public void Rating()
    {
        Application.OpenURL(ratingURL);
    }
}
