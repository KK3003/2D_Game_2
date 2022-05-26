using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;  // this gives access to the [serializable] attribute
using UnityEngine.UI;  // this gives access to the unity UI elements
/// <summary>
///  Groups all the user interface elements together for GameCtrl to use
/// </summary>
/// 
[Serializable]
public class UI 
{
    [Header("Text")]
    public Text txtPotionCount; // for updatinf no. of potions collected
    public Text txtScore;  // foe showing the score in HUD
    public Text txtTimer;  // for showing timer in the HUD

    [Header("Images / Sprites")]
    public Image key0;
    public Image key1;
    public Image key2;
    public Sprite key0Full;
    public Sprite key1Full;
    public Sprite key2Full;
    public Image heart1; // represents one player life
    public Image heart2; // represents one player life
    public Image heart3; // represents one player life
    public Sprite emptyHeart; // shown when one life is lost
    public Sprite fullHeart; // shown when lives are full
    public Slider bossHealth; // the health meter of the boss

    [Header("Popup Menus/Panels")]
    public GameObject panelGameOver;  // the game over panel
    public GameObject levelCompleteMenu; // shown when the level is complete
    public GameObject panelMobileUI; // contains the mobile buttons
    public GameObject panelPause;   // the pause menu
}
