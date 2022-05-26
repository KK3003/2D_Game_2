using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;  // allows to use [serializable] attribute
/// <summary>
/// contains audio clips realted to the player
/// </summary>
/// 

[Serializable]
public class PlayerAudio 
{
    [Header("Part 1")]
    public AudioClip playerJump; // when player jumps
    public AudioClip potionPickup; // when player pickup potion
    public AudioClip fireBullets;// when player fire bullets
    public AudioClip enemyExplosion;// when player kills an enemy
    public AudioClip breakCrates; // when player breaks a crate

    [Header("Part 2")]
    public AudioClip waterSplash; // when palyer falls in water
    public AudioClip powerUp;    // when player collects sanitizer powerup
    public AudioClip keyFound;   // when player collects level keys masks
    public AudioClip enemyHit;  // when player jumps on player head
    public AudioClip playerDied; // when player dies

}
