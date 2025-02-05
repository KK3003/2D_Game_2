﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// manages the behaviour of box fragments
/// </summary>
public class FragmentsCtrl : MonoBehaviour
{
    public Vector3 jumpForce;
    public float destroyDelay;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(jumpForce);

        Destroy(gameObject, destroyDelay);
    }

   
}
