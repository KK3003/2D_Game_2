﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  Updates the HUD when a key is collected by the player
/// </summary>
public class KeyCtrl : MonoBehaviour
{
    public int keyNumber;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.UpdateKeyCount(keyNumber);

            AudioCtrl.instance.KeyFound(gameObject.transform.position); // keyfound mask found sound

            SFXCtrl.instance.ShowKeySparkle(keyNumber);

            Destroy(gameObject);
        }
    }
}
