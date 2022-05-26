using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// handles the potion behaviour when player interacts with it
/// </summary>
public class PotionCtrl : MonoBehaviour
{
    public enum PotionFX
    {
        Vanish,
        Fly
    }

    public PotionFX potionFX;
    public float speed; // speed at which potion flies
    public bool startFlying; // if true, potion will fly to the collector when collected

    GameObject potionMeter;  // this "receives" the potion in the HUD

    private void Start()
    {
        startFlying = false;

        if(potionFX == PotionFX.Fly)
        {
            potionMeter = GameObject.Find("img_Potion_Count");
        }
    }

    private void Update()
    {
        if(startFlying)
        {
            transform.position = Vector3.Lerp(transform.position, potionMeter.transform.position, speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(potionFX == PotionFX.Vanish)
            {
                
                Destroy(gameObject);
            }
            else if(potionFX == PotionFX.Fly)
            {
                gameObject.layer = 0;
                startFlying = true;
            }
        }
    }
}
