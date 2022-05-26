using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // gives access to the [serializable] attribute
/// <summary>
/// data model for the game data
/// </summary>



[Serializable]
public class GameData 
{
    public int potionCount; // tracks the no. of potions collected
    public int score;       // for tracking the score
    public int lives;       // tracks the player lives
    public bool[] keyFound; // for traking which key is found
    public LevelData[] levelData; // for tracking level data like stars awarded, level number  "Class inherited from LevelData class"
    public bool isFirstBoot; // for initializing data when game is started for the forst time
    

    public bool playSound; // to toggle sound
    public bool playMusic; // to toggle music


    
}
