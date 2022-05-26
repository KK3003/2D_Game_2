using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this script receives the potions which fly towards it when player picks them
/// </summary>
public class PotionCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Potion"))
        {
            Destroy(other.gameObject);
        }
    }
}
