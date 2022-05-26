using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  Provides a simple patrolling behviour between two positions
/// </summary>
public class EnemyPatrol : MonoBehaviour
{
    public Transform leftBound, rightBound; // two posions enemy moves
    public float speed;  // speed at which enemy moves
    public float maxDelay, minDelay; // for random delay before enmy turns back

    bool canTurn; // to check when enemy can turn
    float originalSpeed; // helps in flipping enemy direction

    Rigidbody2D rb;
    SpriteRenderer sr; // helps in flipping enemy direction
    Animator anim; // to show the animations

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        SetStartingDirection();

        canTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        FlipOnEdges();
    }

    void Move()
    {
        Vector2 temp = rb.velocity;
        temp.x = speed;
        rb.velocity = temp;
    }    

    void SetStartingDirection()
    {
        if(speed > 0)
        {
            sr.flipX = true;
        }
        else if(speed < 0)
        {
            sr.flipX = false;
        }
    }

    void FlipOnEdges()
    {
        if(sr.flipX && transform.position.x >= rightBound.position.x)
        {
           
           if(canTurn)
            {
                canTurn = false;
                originalSpeed = speed;
                speed = 0;
                StartCoroutine("TurnLeft", originalSpeed);
            }

        }
        else if (!sr.flipX && transform.position.x <= leftBound.position.x)
        {
            if (canTurn)
            {
                canTurn = false;
                originalSpeed = speed;
                speed = 0;
                StartCoroutine("TurnRight", originalSpeed);
            }
        }
    }

    IEnumerator TurnLeft(float originalSpeed)
    {
        anim.SetBool("isIdle", true);
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        anim.SetBool("isIdle", false);
        sr.flipX = false;
         speed = -originalSpeed;
        canTurn = true;
    }

    IEnumerator TurnRight(float originalSpeed)
    {
        anim.SetBool("isIdle", true);
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        anim.SetBool("isIdle", false);
        sr.flipX = true;
        speed = -originalSpeed;
        canTurn = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(leftBound.position, rightBound.position);
    }
}
