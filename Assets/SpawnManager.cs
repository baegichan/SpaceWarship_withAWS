using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviourPunCallbacks
{

    public UnityEvent stageevent;
    float cooltime;
    bool diying;

    public static SpawnManager s_instance;
    private void Awake()
    {
        if(s_instance==null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        SpawnPlayer();
    }



    private void FixedUpdate()
    {
        if (cooltime >0 )
        {
            cooltime -= Time.deltaTime;
        }
        else if (diying ==true)
        {
            SpawnPlayer();
            diying = false;
        }
    }
    public IEnumerator test()
    {
        yield return new WaitForSeconds(5);
    }
    public void DiePlayer()
    {
        cooltime = 5;
        diying = true;
    }
    public void SpawnPlayer()
    {
        if (PhotonNetwork.NickName != "MASTER")
        {
            GameObject player =PhotonNetwork.Instantiate("PLAYER", Vector3.zero, Quaternion.identity);
            if(player.GetComponent<Player>().PV.IsMine)
            {
            
            }
            else
            {
                TargetManager.s_targetmanager.CreateTargetImg(player);
            }
        }
    }
 
}
