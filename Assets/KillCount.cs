using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class KillCount : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string, int> killcount = new Dictionary<string, int>();
    public static KillCount s_instance;
    
    [PunRPC]
    public void PlayerAdd(string player)
    {
        if(killcount.ContainsKey(player) != true)
        {
            killcount.Add(player, 0);
        }
    }
    [PunRPC]
    public void KillUp(string player)
    {
        killcount[player] = killcount[player] + 1;
    }

    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
       
    }
}
