using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Activates the bat when player comes near it
/// </summary>
public class BatActivator : MonoBehaviour
{
    public GameObject bat;  //  referance to bat

    BatAI bbai;  // the ai engine of the bat

    // Start is called before the first frame update
    void Start()
    {
        bbai = bat.GetComponent<BatAI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            bbai.ActivateBat(other.gameObject.transform.position);
        }
    }
}
