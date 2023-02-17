using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Targetinfo : MonoBehaviour
{
    // non player 
    public GameObject worldObject;
    // player
    public GameObject player;
    public Image image;


    
    public void  Set_Targetinfo(GameObject player , GameObject target, Image image)
    {
        this.image = image;
        this.player = player;
        this.worldObject = target;
    }
    public void Update_MainPlayer(GameObject player)
    {
        this.player = player;
    }
    public void Delete_targetui()
    {
        TargetManager.s_targetmanager.Destroy_TargetUI(this);
        Destroy(gameObject);
    }
    void FixedUpdate()

    {
        if (player != null)
        {
            Vector3 obj_postion = Camera.main.WorldToViewportPoint(worldObject.transform.position);
            bool onScreen = obj_postion.z > 0 && obj_postion.x > 0 && obj_postion.x < 1 && obj_postion.y > 0 && obj_postion.y < 1;
            float dis = Vector3.Distance(player.transform.position, worldObject.transform.position);
            if (dis > 10)
            {
                if (image.enabled == false && onScreen == true)
                {
                    image.enabled = true;
                }
                transform.position = Camera.main.WorldToScreenPoint(worldObject.transform.position);
            }
            if (onScreen == false)
            {
                image.enabled = false;
            }
        }
    }
}
