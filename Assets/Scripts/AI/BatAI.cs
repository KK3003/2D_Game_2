using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// The AI for Bat
/// </summary>
public class BatAI : MonoBehaviour
{
    public float destroyBatDelay; // how long to wait before bat is destroyed
    public float batSpeed;        // speed at which bat moves


    public void ActivateBat(Vector3 playerpos)
    {
        transform.DOMove(playerpos, batSpeed, false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            // show explosion
            SFXCtrl.instance.EnemyExplosion(other.gameObject.transform.position);

            // destoy bat
            Destroy(gameObject, destroyBatDelay);


        }
    }
}
