using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// routes mobile input to the corect methods in the PlayerCtrl script
/// </summary>
public class MobileUICtrl : MonoBehaviour
{

    public GameObject player;

    PlayerCtrl playerctrl; // referance to the playerctrl script

    // Start is called before the first frame update
    void Start()
    {
        playerctrl = player.GetComponent<PlayerCtrl>();
    }

   public void MobileMoveLeft()
   {
        playerctrl.MobileMoveLeft();
   }

    public void MobileMoveRight()
    {
        playerctrl.MobileMoveRight();

    }

    public void MobileStop()
    {
        playerctrl.MobileStop();
    }

    public void MobileFireBullets()
    {
        playerctrl.MobileFireBullets();
        
    }

    public void MobileBuyfireBullets()
    {
        playerctrl.MobileBuyFireBullets();
    }

    public void MobileJump()
    {
        playerctrl.MobileJump();
    }
}
