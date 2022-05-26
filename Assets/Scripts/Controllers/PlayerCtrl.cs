using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages:
/// 1.make the player move left and right , jump , flip the playe direction while jumping
/// 2.the player animations
/// </summary>
public class PlayerCtrl : MonoBehaviour
{
    [Tooltip("This is the int which speeds up the player")]
    public int speedBoost; // set this to 5
    public float jumpSpeed; // set this to 600
    public bool isGrounded;
    public Transform feet;
    public float feetRadius;
    public LayerMask whatIsGround;
    public float boxWidth, boxHeight; // cube to check is grounded or not
    public float delayForDoubleJump;
    public Transform leftBulletSpawnPos, rightBulletSpawnPos;  // positions from where bullets will be fired
    public GameObject leftBullet, rightBullet; // bullets will be placed here
    public bool isJumping;
    public bool isStuck;
    public int health;

    public bool SFXOn;
    public bool canFire;
    public bool canBuyFire;
    bool canDoubleJump;
    public bool leftPressed, rightPressed;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    ObjectPooler objectpooler;

     public GameCtrl gamectrl;
    

   private void Awake()
   {
        if(PlayerPrefs.HasKey("CPX"))
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("CPX"), PlayerPrefs.GetFloat("CPY"), transform.position.z);
        }
   }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        objectpooler = ObjectPooler.instance;
    }

    // Update is called once per frame
    void Update()
    {

        // isGrounded = Physics2D.OverlapCircle(feet.position, feetRadius, whatIsGround);

        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(boxWidth, boxHeight), 360.0f, whatIsGround); // help to figure out when the player is on ground if any overlapping happen between gizmos circle and 
                                                                                                                                                  //ground layer isGrounded will become true
        float playerSpeed = Input.GetAxisRaw("Horizontal"); // value will be 1, 0, -1
        playerSpeed *= speedBoost; // playerSpeed = playerSpeed * playerBoost


        if(playerSpeed != 0) // making player move in horizontal direction and stop
        {
            MoveHorizontal(playerSpeed);
        }
        else
        {
            StopMoving();
        }

        if(Input.GetButtonDown("Jump")) // making player jump
        {
            Jump();
        }

        if(Input.GetButtonDown("Fire1"))
        {
            fireBullets();
        }

        showFalling();

        if(leftPressed)
        {
            MoveHorizontal(-speedBoost);
        }

        if (rightPressed)
        {
            MoveHorizontal(speedBoost);
        }
    }

    void OnDrawGizmos()   // draw gizmos at player feet for visual debugging
    {
        //Gizmos.DrawWireSphere(feet.position, feetRadius);

        Gizmos.DrawWireCube(feet.position, new Vector3(boxWidth, boxHeight, 0)); // box to check is grounded or not
    }

    void MoveHorizontal(float playerSpeed)  // moves the player in horizontal direction
    {
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);

        if(playerSpeed < 0)
        {
            sr.flipX = true;
        }
        else if(playerSpeed > 0)
        {
            sr.flipX = false;
        }

        if(!isJumping)
        {
            anim.SetInteger("State", 1); // sets animation state to running

        }
    }

    void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        if(!isJumping)
        {
            anim.SetInteger("State", 0); // sets animation state to idle

        }

    }

    void showFalling()
    {
        if(rb.velocity.y < 0)
        {
            anim.SetInteger("State", 3);
        }
    }

    void Jump()
    {
        if(isGrounded)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpSpeed)); // make the player jump in y direction

            anim.SetInteger("State", 2); // sets animation to jump

            // play the jump sound
            AudioCtrl.instance.PlayerJump(gameObject.transform.position);

            Invoke("EnableDoubleJump", delayForDoubleJump);
        }

        if(canDoubleJump && !isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpSpeed)); // make the player jump in y direction
            anim.SetInteger("State", 2); // sets animation to jump

            // play the jump sound
            AudioCtrl.instance.PlayerJump(gameObject.transform.position);

            canDoubleJump = false;

        }
    }

    void EnableDoubleJump() // to enable double jump
    {
        canDoubleJump = true;
    }

    void fireBullets()
    { 
        if(canFire)
        {
            // makes the player fire bullets in left directon
            if (sr.flipX)
            {
                // Instantiate(leftBullet, leftBulletSpawnPos.position, Quaternion.identity);
                objectpooler.SpawnFromPool("PlayerBulle", leftBulletSpawnPos.position, Quaternion.identity);

               // gamectrl.MinusScore();
               
            }

            // makes the player fire bullets in right direction
            if (!sr.flipX)
            {
               // Instantiate(rightBullet, rightBulletSpawnPos.position, Quaternion.identity);
                objectpooler.SpawnFromPool("PlayerBullet", rightBulletSpawnPos.position, Quaternion.identity);
            }

            AudioCtrl.instance.FireBullets(gameObject.transform.position); // firing bullets sound
        }
    }


    void fireBuyBullets()
    {
        if (canBuyFire)
        {
            // makes the player fire bullets in left directon
            if (sr.flipX)
            {
                // Instantiate(leftBullet, leftBulletSpawnPos.position, Quaternion.identity);
                objectpooler.SpawnFromPool("LeftBulletBuy", leftBulletSpawnPos.position, Quaternion.identity);

                gamectrl.MinusScore();

            }

            // makes the player fire bullets in right direction
            if (!sr.flipX)
            {
                // Instantiate(rightBullet, rightBulletSpawnPos.position, Quaternion.identity);
                objectpooler.SpawnFromPool("RightBulletBuy", rightBulletSpawnPos.position, Quaternion.identity);

                gamectrl.MinusScore();
            }

            AudioCtrl.instance.FireBullets(gameObject.transform.position); // firing bullets sound
        }
    }


    public void MobileMoveLeft()
    {
        leftPressed = true;
    }

    public void MobileMoveRight()
    {
        rightPressed = true;

    }

    public void MobileStop()
    {
        leftPressed = false;
        rightPressed = false;

        StopMoving();
    }

    public void MobileFireBullets()
    {
        fireBullets();
        
    }

    public void MobileBuyFireBullets()
    {
        fireBuyBullets();
    }

    public void MobileJump()
    {
        Jump();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("infectedman") || other.gameObject.CompareTag("LevelOneBoss"))
        {
            GameCtrl.instance.PlayerDiedAnimation(gameObject);

            AudioCtrl.instance.PlayerDied(gameObject.transform.position); // player dead sound
        }

        if(other.gameObject.CompareTag("smallvirus"))
        {
            if(health > 0)
            {
                health--;
                sr.color = Color.red;  
                Invoke("RestoreColor", 0.5f);
                //Debug.Log("health" + health);
            }
            else
            {
                GameCtrl.instance.PlayerDiedAnimation(gameObject);

                AudioCtrl.instance.PlayerDied(gameObject.transform.position); // player dead sound

                health = 1;
            }
        }

        if (other.gameObject.CompareTag("BigPotion"))
        {
            GameCtrl.instance.UpdatePotionCount();

            SFXCtrl.instance.ShowSanitizerSparkle(other.gameObject.transform.position);

            Destroy(other.gameObject);

            GameCtrl.instance.UpdateScore(GameCtrl.Item.BigPotion);

            AudioCtrl.instance.PotionPickup(gameObject.transform.position); // potion pickup sound
        }
    }


    void RestoreColor()
    {
        sr.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         switch(other.gameObject.tag)
         {
            case "Potion":
                if(SFXOn)
                {
                    SFXCtrl.instance.ShowPotionSparkle(other.gameObject.transform.position);

                    GameCtrl.instance.UpdateScore(GameCtrl.Item.Potion);

                    AudioCtrl.instance.PotionPickup(gameObject.transform.position); // potion pickup sound
                }
                GameCtrl.instance.UpdatePotionCount();
                break;

            case "Water":
                // show the splash effect
                SFXCtrl.instance.ShowSplash(other.gameObject.transform.position);

                //player falls in water audio
                AudioCtrl.instance.WaterSplash(gameObject.transform.position);

                
                break;

            case "Enemy":
                GameCtrl.instance.PlayerDiedAnimation(gameObject);

                AudioCtrl.instance.PlayerDied(gameObject.transform.position); // player dead sound
                break;


            case "Powerup_Sanitizer":
                canFire = true;
                Vector3 powerupPos = other.gameObject.transform.position;
                AudioCtrl.instance.PowerUp(gameObject.transform.position); // power up sound
                Destroy(other.gameObject);
                if (SFXOn)
                {
                    SFXCtrl.instance.ShowSanitizerSparkle(powerupPos);
                }
                break;

            case "BossKey":
                GameCtrl.instance.ShowLever();
                break;
            default:
                break;
         }
    }
}
