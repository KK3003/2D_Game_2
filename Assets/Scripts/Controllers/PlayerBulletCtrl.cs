using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// handles the player's bullet movement and collisions with enemies
/// </summary>
public class PlayerBulletCtrl : MonoBehaviour
{
    public Vector2 velocity;

    PlayerCtrl playerctrl;


    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = velocity;
        Invoke("DestroyBullet", 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("infectedman") || other.gameObject.CompareTag("smallvirus"))
        {
            GameCtrl.instance.BulletHitEnemy(other.gameObject.transform);
            gameObject.SetActive(false);
           // Destroy(gameObject);
        }
        
        else if(!other.gameObject.CompareTag("Player"))
        {
               gameObject.SetActive(false);
            // Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("infectedman") || other.gameObject.CompareTag("smallvirus"))
        {
            GameCtrl.instance.BulletHitEnemy(other.gameObject.transform);
            gameObject.SetActive(false);
          //  Destroy(gameObject);
        }

    }

    void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}
