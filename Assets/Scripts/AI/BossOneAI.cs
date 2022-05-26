using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// The AI engine of Level One Boss
/// </summary>

public class BossOneAI : MonoBehaviour
{
    public float jumpSpeed; // the jumping speed of the level boss in Y axis
    public int startJumpingAt; // the health level at which boss start jumping
    public int jumpDelay; // delay in seconds before another jump
    public int health; // the health of the level boss
    public Slider bossHealth; // health meter of the boss level
    public GameObject bossBullett;  // the bullet which boss fires
    public float delayBeforeFiring; // delay in seconds before firing bullet

    Rigidbody2D rb;
    SpriteRenderer sr;
    Vector3 bulletSpawnPos; // this is from where bullet will be fired
    bool canFire, isJumping; // to check when boss can fire and jump


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        canFire = false;

#pragma warning disable CS0618 // Type or member is obsolete
        bulletSpawnPos = gameObject.transform.FindChild("BulletSpawnPos").transform.position;
#pragma warning restore CS0618 // Type or member is obsolete

        Invoke("Reload", Random.Range(1f, delayBeforeFiring));
    }

    // Update is called once per frame
    void Update()
    {
        if(canFire)
        {
            FireBullets();
            canFire = false;

            if(health < startJumpingAt && !isJumping)
            {
                InvokeRepeating("Jump", 0, jumpDelay);
                isJumping = true;
            }
        }
    }

    void Reload()
    {
        canFire = true;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpSpeed));
    }

    void FireBullets()
    {
        Instantiate(bossBullett, bulletSpawnPos, Quaternion.identity);

        Invoke("Reload", delayBeforeFiring);
    }

    void RestoreColor()
    {
        sr.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("PlayerBullet"))
        {
            if(health == 0)
            {
                GameCtrl.instance.BulletHitEnemy(gameObject.transform);
            }

            if(health > 0)
            {
                health--;

                bossHealth.value = (float) health;

                sr.color = Color.red;

                Invoke("RestoreColor", 0.1f);
            }
        }
    }
}
