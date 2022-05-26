using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// controls the enemy behaviour when player jumps on the enemy head
/// </summary>
public class EnemyHeadCtrl : MonoBehaviour
{
    public GameObject enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerFeet"))
        {
            GameCtrl.instance.PlayerStompsEnemy(enemy);

            AudioCtrl.instance.EnemyHit(enemy.transform.position);// when player jump on enemy head sound

            SFXCtrl.instance.ShowEnemyPoof(enemy.transform.position);
        }
    }
}
