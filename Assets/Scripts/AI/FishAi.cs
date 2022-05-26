using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// the AI engine for fish
/// </summary>
public class FishAi : MonoBehaviour
{
    public float jumpSpeed; // the jump speed in the Y axis for the fish


    Rigidbody2D rb;
    SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        FishJump();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y > 0)
        {
            sr.flipY = false; // facing up
        }
        else
        {
            sr.flipY = true; // facing down
        }
    }

    public void FishJump()
    {
        rb.AddForce(new Vector2(0, jumpSpeed));
    }
}
