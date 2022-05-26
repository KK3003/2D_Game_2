using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// destroys any gameobject that comes in contact with it
/// for the player level is restarted
/// </summary>
public class GarbageCtrl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.PlayerDied(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
