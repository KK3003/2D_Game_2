using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// controls the plaform's dropping behaviour
/// </summary>
public class DroppingPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    public float droppingDelay;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerFeet"))
        {
            Invoke("StartDropping", droppingDelay);
        }
    }

    void StartDropping()
    {
        rb.isKinematic = false;
    }
}
