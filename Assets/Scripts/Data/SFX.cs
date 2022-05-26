using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// groups the particle effects used in the game
/// </summary>
/// 

[Serializable]
public class SFX 
{
    public GameObject sfx_potion_pickup;      // shown when the player picks potion
    public GameObject sfx_sanitizer_pickup;   // shown when the player picks the sanitizer powerup
    public GameObject sfx_playerLands;       // shown when the player lands on the ground
    public GameObject sfx_fragment_1;       // box fragment shown when the crate breaks
    public GameObject sfx_fragment_2;       // box fragment shown when the crate breaks
    public GameObject sfx_splash;       // shows the splash effect
    public GameObject sfx_enemy_explosion; // shown when the bullet hits the enemy



}
