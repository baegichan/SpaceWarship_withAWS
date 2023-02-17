using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class Score : MonoBehaviour
{
    

    public Text target;
 
    public enum data_info
    {
        nickname,
        kill_score,
        death_score,
        kd
    }
    public data_info data;
    private void Start()
    {
     
        Set_score();
    }
    public void Set_score()
    {
        switch (data)
        {
            case data_info.nickname:
                target.text = PlayerData.s_playerdata.Get_playernickname().ToString();
                break;
            case data_info.kill_score:
                target.text = PlayerData.s_playerdata.Get_killscore().ToString();
                break;
            case data_info.death_score:
                target.text = PlayerData.s_playerdata.Get_deathscore().ToString();
                break;
            case data_info.kd:
                float kill = (float)PlayerData.s_playerdata.Get_killscore();
                float death = (float)PlayerData.s_playerdata.Get_killscore();
                if (death == 0 || kill == 0)
                {
                    target.text = "0%";
                }
                else
                {
                    target.text = (Math.Round(kill / kill+death, 2) * 100).ToString() + "%";
                }
                break;
        }

    }
}
