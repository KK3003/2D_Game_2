using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// destroy the gameobject after a specified delay
/// </summary>
public class DestroyWithDelay : MonoBehaviour
{

    public float delay; // set this to 1.5

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }

  
}
