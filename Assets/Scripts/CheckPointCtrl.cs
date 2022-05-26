using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Saves the current position of the player into the PlayerPrefs
/// </summary>
public class CheckPointCtrl : MonoBehaviour
{

    public GameObject CantGoBack;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("CPX", other.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("CPY", other.gameObject.transform.position.y);
            PlayerPrefs.SetInt("Checkpoint", 1);
            CantGoBack.SetActive(true);
        }
    }

    
}
