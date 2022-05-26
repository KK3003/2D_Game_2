using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// help to show dust particle effecs when player lands
/// </summary>
public class FeetCtrl : MonoBehaviour
{
    public GameObject player;

    PlayerCtrl playerctrl;

    public Transform dustParticlePos;

    private void Start()
    {
        playerctrl = gameObject.transform.parent.gameObject.GetComponent<PlayerCtrl>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            SFXCtrl.instance.ShowPlayerLanding(dustParticlePos.position);

            playerctrl.isJumping = false;
        }

        if (other.gameObject.CompareTag("Platform"))
        {
            SFXCtrl.instance.ShowPlayerLanding(dustParticlePos.position);

            playerctrl.isJumping = false;

            player.transform.parent = other.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            player.transform.parent = null;
        }
    }
}
