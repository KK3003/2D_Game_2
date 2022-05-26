using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// checks when player is stuck
/// </summary>
public class PlayerStuck : MonoBehaviour
{
    public GameObject player;

    PlayerCtrl playerctrl;

    // Start is called before the first frame update
    void Start()
    {
        playerctrl = player.GetComponent<PlayerCtrl>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerctrl.isStuck = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerctrl.isStuck = false;
    }
}
