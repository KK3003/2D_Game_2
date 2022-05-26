using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  provides the parallax effect
/// </summary>
public class Parallax : MonoBehaviour
{
    public GameObject player;

    public float speed;   // at this speed bg1 moves set this to 0.001
    float offSetX; // secret of the horizontal parallax
    Material mat; // material attached to the renderer of quad

    PlayerCtrl playerctrl;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        playerctrl = player.GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        // offSetX += 0.010f;

       if(!playerctrl.isStuck)
        {
            // shows parallax effect for keyboard and joystick
            offSetX += Input.GetAxisRaw("Horizontal") * speed;

            // handles the mobile parallax
            if(playerctrl.leftPressed)
            {
                offSetX += -speed;
            }

            if(playerctrl.rightPressed)
            {
                offSetX += speed;
            }

            mat.SetTextureOffset("_MainTex", new Vector2(offSetX, 0));
        }
    }
}
