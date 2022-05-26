using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// releases the potion from the cage
/// </summary>
public class LeverCtrl : MonoBehaviour
{
    public GameObject potion;  // the gameobjevt potion
    public Vector2 jumpSpeed; // speed at which potion jumps out of the cage
    public Sprite leverPulled; // the image of the pulled lever

    public GameObject[] stairs; // stairs through which the potion gonna jump

    

    Rigidbody2D rb;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = potion.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            rb.AddForce(jumpSpeed);

            foreach(GameObject stair in stairs)
            {
                stair.GetComponent<BoxCollider2D>().enabled = false;
            }

            SFXCtrl.instance.ShowPlayerLanding(gameObject.transform.position);

            sr.sprite = leverPulled;

            Invoke("deleteCheckpointsboss", 0.2f);

            

            Invoke("ShowLevelCompleteMenu", 3f);
        }
    }

   

    void deleteCheckpointsboss()
    {
        GameCtrl.instance.DeleteCheckpoints();
    }

    void ShowLevelCompleteMenu()
    {
        GameCtrl.instance.LevelComplete();
    }
}
