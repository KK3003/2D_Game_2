using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // gets access to the unity UI elements
using System.IO; // foe working with files
using System.Runtime.Serialization.Formatters.Binary; // RSFB helps serialization
using DG.Tweening;


/// <summary>
/// manages the important gameplay features like keeping the score , restarting levels,
/// saving/loading data, updatingg the HUD etc.
/// </summary>
public class GameCtrl : MonoBehaviour
{
    public static GameCtrl instance;
    public float restartDelay;
    [HideInInspector]
    public GameData data;   // to work with game data in inspector
    public UI ui;           // foe neatly arranging UI elements
    public GameObject bigPotion; // reward after killing the enemy
    public GameObject lever; // the lever which releases the potion
    public GameObject enemySpawner; // spawns the enemies during the boss battle
    public GameObject player; // the player character
    public GameObject invisiblePlatform; // the platform which will enabled after collecting mask


    // can delete
    public int healthvirus;
    
    public int potionValue;  // value of the red potion
    public int bigPotionVlaue;  // value of big potion
    public int enemyValue;  // value of enemy
    public float maxTime; // max time Allowed to complete level
    public Button btnPause; // to disable btn pause

    public Button FireBuyBullets;

    public PlayerCtrl playerctl;

    CheckPointCtrl checkpoint;


    public enum Item
    {
        Potion,
        BigPotion,
        Enemy
    }


    public enum Items
    {
        BigpotionCount
    }



    string dataFilePath;   // path to store datafile
    BinaryFormatter bf;    // helps in saving loading to binary files
    float timeLeft;        // time left before the timer goes off
    public bool timerOn;   // checks if timer should be on or off
    bool isPaused;  // to pause/unpause the game


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        bf = new BinaryFormatter();

        dataFilePath = Application.persistentDataPath + "/game.dat";

        Debug.Log(dataFilePath);
    }

    // Start is called before the first frame update
    void Start()
    {
        

        DataCtrl.instance.RefreshData();
        data = DataCtrl.instance.data;
        RefreshUI();

        

        // LevelComplete();

        timeLeft = maxTime;

        timerOn = true;

        isPaused = false;

        HandleFirstBoot();

        UpdateHearts();

        ui.bossHealth.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        if(isPaused)
        {
            // set time.timescle = 0
            Time.timeScale = 0;
        }
        else
        {
            // set time.timescale = 1
            Time.timeScale = 1;
        }

        if(timeLeft > 0 && timerOn)
        {
            UpdateTimer();
        }

        
    }


    public void RefreshUI()
    {
       // data.score = 0;
        ui.txtPotionCount.text = "x" + data.potionCount;
        if(PlayerPrefs.GetInt("Checkpoint",1)== 1)
        {
            ui.txtScore.text = "Score:" + data.score;
            playerctl.canFire = true;
        }
        else
        {
            data.score = 0;
        }
       // ui.txtScore.text = "Score:" + data.score;
    }

    private void OnEnable()
    {
        Debug.Log("Data Loaded");
        RefreshUI();
    }


    private void OnDisable()
    {
        Debug.Log("Data Saved");
        DataCtrl.instance.SaveData(data);

        Time.timeScale = 1;

        // hide the ad banner while changing scenes
        AdsCtrl.instance.HideBanner();
    }

    /// <summary>
    /// Deletes the checkpoints
    /// </summary>
  public void DeleteCheckpoints()
  {
        PlayerPrefs.DeleteKey("CPX");
        PlayerPrefs.DeleteKey("CPY");

        checkpoint.CantGoBack.SetActive(false);
  }

    /// <summary>
    /// Saves the stars awarded for the level
    /// </summary>
    /// <param name="levelNumber"></param>
    /// <param name="numOfStars"></param>
    public void SetStarsAwarded(int levelNumber, int numOfStars)
    {
        data.levelData[levelNumber].starsAwarded = numOfStars;

        // test
        Debug.Log("No. of stars awarded = " + data.levelData[levelNumber].starsAwarded);
    }

    /// <summary>
    ///  unlocks the specified level
    /// </summary>
    /// <param name="levelNumber"></param>
    public void UnlockLevel(int levelNumber)
    {
        if((levelNumber+1) <= (data.levelData.Length-1))
        {
            data.levelData[levelNumber+1].isUnlocked = true;
            DataCtrl.instance.SaveData(data);
        }
        
    }

    /// <summary>
    /// Gets the current score for game complete menu
    /// </summary>
    public int GetScore()
    {
        return data.score;
    }

    public void MinusScore()
    {
       
        int minusvalue = 10;

        if (data.potionCount >= 10)
        {
            FireBuyBullets.interactable = true;
            playerctl.canBuyFire = true;
            data.potionCount -= minusvalue;
            ui.txtPotionCount.text = "x" + data.potionCount;
        }
        else
        {
            FireBuyBullets.interactable = false;
            playerctl.canBuyFire = false;
        }

    }

    /// <summary>
    /// restart the level when player dies
    /// </summary>
    public void PlayerDied(GameObject player)
    {
        player.SetActive(false);

        CheckLives();
       // Invoke("RestartLevel", restartDelay);
    }

    public void PlayerDiedAnimation(GameObject player)
    {
        // throw the player back in the air
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-150f, 400f));

        // rotate a player bit
        player.transform.Rotate(new Vector3(0, 0, 45f));

        // disable the platerctrl script
        player.GetComponent<PlayerCtrl>().enabled = false;

        // disable the colliders attached to the player so player ca go through ground
        foreach(Collider2D c2d in player.transform.GetComponents<Collider2D>())
        {
            c2d.enabled = false;
        }

        // disable the child gameobjects attached to the player
        foreach(Transform child in player.transform)
        {
            child.gameObject.SetActive(false);
        }

        // disable the camers attached to the player
        Camera.main.GetComponent<CameraCtrl>().enabled = false;

        // set the velocity of player to 0
        rb.velocity = Vector2.zero;

        // restart level
        StartCoroutine("PauseBeforeReload", player);

    }


    public void PlayerStompsEnemy(GameObject enemy)
    {
        // change the enemy's tag 
        enemy.tag = "Untagged";

        // destroy the enemy
        Destroy(enemy);

        // update the score
        UpdateScore(Item.Enemy);
    }

    IEnumerator PauseBeforeReload(GameObject player)
    {
        yield return new WaitForSeconds(1.5f); // causes a specified delay
        PlayerDied(player);
    }

    /// <summary>
    /// player falls in water
    /// </summary>
    public void PlayerDrowned(GameObject player)
    {
        //Invoke("RestartLevel", restartDelay);
        CheckLives();
    }


    public void UpdatePotionCount()
    {
        data.potionCount += 1;

        ui.txtPotionCount.text = "x" + data.potionCount;

       
    }

    

    public void UpdateScore(Item item)
    {

        int itemValue = 0;

        switch (item)
        {
            case Item.BigPotion:
                itemValue = bigPotionVlaue;
                break;

            case Item.Potion:
                itemValue = potionValue;
                break;

            case Item.Enemy:
                itemValue = enemyValue;
                break;


            default:
                break;
        }

        data.score += itemValue;

        ui.txtScore.text = "Score:" + data.score;

        
    }

    

    /// <summary>
    /// called when player hits the enemy
    /// </summary>
    /// <param name="enemy"></param>
    public void BulletHitEnemy(Transform enemy)
    {
        Vector3 pos = enemy.position;
        pos.z = 20f;

        if(enemy.gameObject.CompareTag("Enemy"))
        {
            // show the enemy explosion sfx

            SFXCtrl.instance.EnemyExplosion(pos);

            // show the bigPotion after kill
            Instantiate(bigPotion, pos, Quaternion.identity);

            // enemy dead sound
            AudioCtrl.instance.EnemyExplosion(pos);

            // destroy enemy
            Destroy(enemy.gameObject);
        }
        if(enemy.gameObject.CompareTag("smallvirus"))
        {
            // show the enemy explosion sfx

            SFXCtrl.instance.EnemyExplosion(pos);

            // show the bigPotion after kill
            Instantiate(bigPotion, pos, Quaternion.identity);

            // enemy dead sound
            AudioCtrl.instance.EnemyExplosion(pos);

            // destroy enemy
            Destroy(enemy.gameObject);

            playerctl.health = 1;
        }
        else if (enemy.gameObject.CompareTag("infectedman"))
        {
            if (healthvirus > 0)
            {
                healthvirus--;
                SFXCtrl.instance.ShowEnemyPoof(pos);
            }
            else
            {
                // show the enemy explosion sfx

                SFXCtrl.instance.EnemyExplosion(pos);

                // show the bigPotion after kill
                Instantiate(bigPotion, pos, Quaternion.identity);

                // enemy dead sound
                AudioCtrl.instance.EnemyExplosion(pos);

                // destroy enemy
                Destroy(enemy.gameObject);

                healthvirus = 2;
            }
        }
        else
        {
            // show the enemy explosion sfx

            SFXCtrl.instance.EnemyExplosion(pos);

            // show the bigPotion after kill
            Instantiate(bigPotion, pos, Quaternion.identity);

            // enemy dead sound
            AudioCtrl.instance.EnemyExplosion(pos);

            // destroy enemy
            Destroy(enemy.gameObject);
        }
    }

    public void UpdateKeyCount(int keyNumber)
    {
        data.keyFound[keyNumber] = true;

        if(keyNumber == 0)
        {
            ui.key0.sprite = ui.key0Full;
        }else if (keyNumber == 1)
        {
            ui.key1.sprite = ui.key1Full;
        }
        else if (keyNumber == 2)
        {
            ui.key2.sprite = ui.key2Full;
        }

        if(data.keyFound[0] && data.keyFound[1])
        {
            ShowInvisiblePlatform();
        }
    }

    void ShowInvisiblePlatform()
    {
        invisiblePlatform.SetActive(true);
        SFXCtrl.instance.ShowPlayerLanding(invisiblePlatform.transform.position);

        timerOn = false;

        ui.bossHealth.gameObject.SetActive(true);
    }


    public void LevelComplete()
    {
        // can delete if problem occurs
        data.lives = 3;
        PlayerPrefs.SetInt("Checkpoint", 0);

        if (timerOn)
        {
            timerOn = false;
        }
        ui.panelMobileUI.SetActive(false);
        // show an Interstitial ad
        AdsCtrl.instance.ShowINterstitial();
        ui.levelCompleteMenu.SetActive(true);
        player.SetActive(false);
        DeleteCheckpoints();
        Dialouge.instance.deleteDialouge();
        
    }


    void RestartLevel()
    {
        if (data.lives == 0)
        {
            DeleteCheckpoints();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;

        ui.txtTimer.text = "Timer:" + (int)timeLeft;

        if(timeLeft<=0)
        {
            ui.txtTimer.text = "Timer: 0";

            // inform the GameCtrl to do the needful
            GameObject player = GameObject.FindGameObjectWithTag("Player") as GameObject;
            PlayerDied(player);
        }
    }


    void HandleFirstBoot()
    {
        if(data.isFirstBoot)
        {
            //set lives to 3
            data.lives = 0;

            // set number of coins to 0
            data.potionCount = 0;

            // set keys collected to 0
            data.keyFound[0] = false;
            data.keyFound[1] = false;
            data.keyFound[2] = false;

            // set score to 0
            data.score = 0;

            // set isFirstBoot to false
            data.isFirstBoot = false;
        }
    }

    void UpdateHearts()
    {
        if(data.lives == 3)
        {
            ui.heart1.sprite = ui.fullHeart;
            ui.heart2.sprite = ui.fullHeart;
            ui.heart3.sprite = ui.fullHeart;
        }
        if(data.lives == 2)
        {
            ui.heart1.sprite = ui.emptyHeart;
        }

        if (data.lives == 1)
        {
            ui.heart1.sprite = ui.emptyHeart;
            ui.heart2.sprite = ui.emptyHeart;
        }
    }

    void CheckLives()
    {
        int updatedLives = data.lives;
        updatedLives -= 1;
        data.lives = updatedLives;

        if(data.lives == 0)
        {
            data.lives = 3;
            DataCtrl.instance.SaveData(data);
            Invoke("GameOver", restartDelay);
        }
        else
        {
            DataCtrl.instance.SaveData(data);
            Invoke("RestartLevel", restartDelay);
        }
    }

    public void StopCameraFollow()
    {
        Camera.main.GetComponent<CameraCtrl>().enabled = false;
        player.GetComponent<PlayerCtrl>().isStuck = true; // stops parallax
#pragma warning disable CS0618 // Type or member is obsolete
        player.transform.FindChild("Left_Check").gameObject.SetActive(false);
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
        player.transform.FindChild("Right_Check").gameObject.SetActive(false);
#pragma warning restore CS0618 // Type or member is obsolete

    }

    public void ShowLever()
    {
        lever.SetActive(true);

        DeactivateEnemySpawner();

        SFXCtrl.instance.ShowPlayerLanding(lever.gameObject.transform.position);

        AudioCtrl.instance.EnemyExplosion(lever.gameObject.transform.position);

    }

    public void ActivateEnemySpawner()
    {
        enemySpawner.SetActive(true);
    }

    public void DeactivateEnemySpawner()
    {
        enemySpawner.SetActive(false);
    }

    void GameOver()
    {
        // todo
        //ui.panelGameOver.SetActive(true);

        if(timerOn)
        {
            timerOn = false;
        }

        // show game over menu with sliding animation
        ui.panelGameOver.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 0.7f, false);

        // show the admob banner
        AdsCtrl.instance.ShowBanner();

        // show an Interstitial ad
        AdsCtrl.instance.ShowINterstitial();

        PlayerPrefs.SetInt("Checkpoint", 0);

        DeleteCheckpoints();
    }

     

    /// <summary>
    /// shoes the pause panel
    /// </summary>
    public void ShowPausePanel()
    {
        if(ui.panelMobileUI.activeInHierarchy)
        {
            ui.panelMobileUI.SetActive(false);
        }

        // show the pause menu
        ui.panelPause.SetActive(true);

        // animate the pause panel
        ui.panelPause.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 0.7f, false);

        // show the admob banner
        AdsCtrl.instance.ShowBanner();

        // show an Interstitial ad
        AdsCtrl.instance.ShowINterstitial();

        Invoke("SetPause", 1.1f);

        btnPause.GetComponent<Button>().interactable = false;
    }

    void SetPause()
    {
        // set the bool
        isPaused = true;
    }

    /// <summary>
    /// Hides the pause Panel
    /// </summary>
   public void HidePausePanel()
   {
        isPaused = false;

        if (!ui.panelMobileUI.activeInHierarchy)
        {
            ui.panelMobileUI.SetActive(true);
        }
        
        // animate the pause panel
        ui.panelPause.gameObject.GetComponent<RectTransform>().DOAnchorPosY(600f, 0.7f, false);

        // hide the admob banner
        AdsCtrl.instance.HideBanner();

        btnPause.GetComponent<Button>().interactable = true;
        // hide the pause menu
        //ui.panelPause.SetActive(false);
    }
}
