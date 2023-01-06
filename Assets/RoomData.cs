using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class RoomData : MonoBehaviour
{
    public string roomname;
    public int currentplayer;
    public int maxplayer;
    public Text roomname_Text;
    public Text player_Text;

    public RoomInfo _roomInfo;
    public RoomInfo RoomInfo{
    get{
            return _roomInfo;
    }
    set{
            _roomInfo = value;
            roomname = _roomInfo.Name;
            maxplayer = _roomInfo.MaxPlayers;
            currentplayer = _roomInfo.PlayerCount;
            Update_UI();
        }
    }
   public void Join(Text roomname)
   {
        Launcher.s_instance.JoinRoom(roomname);
   }
    void Update_UI()
    {

        Hashtable cp = _roomInfo.CustomProperties;
        if((string)cp["MASTER"]=="TRUE"){
            roomname_Text.text = roomname;
            player_Text.text = currentplayer-1 + "/" + maxplayer;
        }
        else
        {
            roomname_Text.text = roomname;
            player_Text.text = currentplayer + "/" + maxplayer;
        }
       
    }
    void EnterRoom(string roomName){
        PhotonNetwork.JoinOrCreateRoom(roomname, new RoomOptions { MaxPlayers = 5 },null);
    }

}
