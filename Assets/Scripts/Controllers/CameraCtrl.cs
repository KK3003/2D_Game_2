using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// makes the camera follow the player
/// </summary>
public class CameraCtrl : MonoBehaviour
{
    public Transform player;
   // public float yOffset;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // makes the camera follow player in x direction
       transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);

        // makes the camera follow player in x and y direction
       // transform.position = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
    }
}
