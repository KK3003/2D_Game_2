using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  handles the particle effects and other special effects for game
/// </summary>
/// 

public class SFXCtrl : MonoBehaviour
{
    public SFX sfx;
    public static SFXCtrl instance; //allows other scripts to access public methods in this class without creating object with this class
    public Transform key0, key1, key2; // positions of the keys in HUD

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    

    /// <summary>
    /// shows the Potion sparkle effect when the potion is collected
    /// </summary>
    public void ShowPotionSparkle(Vector3 pos)
    {
        Instantiate(sfx.sfx_potion_pickup, pos, Quaternion.identity);
    }


    /// <summary>
    /// shows the enemy explosion  effect when the potion is collected
    /// </summary>
    public void EnemyExplosion(Vector3 pos)
    {
        Instantiate(sfx.sfx_enemy_explosion, pos, Quaternion.identity);
    }


    public void ShowKeySparkle(int keyNumber)
    {
        Vector3 pos = Vector3.zero;
        if(keyNumber==0)
        {
            pos = key0.position;
        }else if(keyNumber==1)
        {
            pos = key1.position;
        }else if(keyNumber==2)
        {
            pos = key2.position;
        }
        Instantiate(sfx.sfx_sanitizer_pickup, pos, Quaternion.identity);
    }

    /// <summary>
    /// shows the sanitizer powerup sparkle effect when the powerup is collected
    /// </summary>
    public void ShowSanitizerSparkle(Vector3 pos)
    {
        Instantiate(sfx.sfx_sanitizer_pickup, pos, Quaternion.identity);
    }

    /// <summary>
    /// shows the player landing dust particle effect
    /// </summary>
    public void ShowPlayerLanding(Vector3 pos)
    {
        Instantiate(sfx.sfx_playerLands, pos, Quaternion.identity);
    }

    /// <summary>
    ///  shows the enemy explosion effect when player jumps on enemy head
    /// </summary>
    /// <param name="pos"></param>
    public void ShowEnemyPoof(Vector3 pos)
    {
        Instantiate(sfx.sfx_playerLands, pos, Quaternion.identity);
    }

    /// <summary>
    /// shows the splash particle effect when player lands in the water
    /// </summary>
    public void ShowSplash(Vector3 pos)
    {
        Instantiate(sfx.sfx_splash, pos, Quaternion.identity);
    }


    /// <summary>
    /// shows the box breaking effect
    /// </summary>
    public void HandleBoxBreaking(Vector3 pos)
    {
        //position of the first fragment
        Vector3 pos1 = pos;
        pos1.x -= 0.3f;
        //position of the second fragment
        Vector3 pos2 = pos;
        pos2.x += 0.3f;


        //position of the third fragment
        Vector3 pos3 = pos;
        pos3.x -= 0.3f;
        pos3.y -= 0.3f;


        //position of the fourth fragment
        Vector3 pos4 = pos;
        pos4.x += 0.3f;
        pos4.y += 0.3f;



        // show the four broken fragments
        // these fragments or broken pieces have jump scripts attached
        // so after instantiation, they will jump apart
        Instantiate(sfx.sfx_fragment_1, pos1, Quaternion.identity);
        Instantiate(sfx.sfx_fragment_2, pos2, Quaternion.identity);
        Instantiate(sfx.sfx_fragment_2, pos3, Quaternion.identity);
        Instantiate(sfx.sfx_fragment_1, pos4, Quaternion.identity);

        AudioCtrl.instance.BreakableCrate(pos);
    }
}
