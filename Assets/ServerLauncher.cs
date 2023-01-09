using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class ServerLauncher : MonoBehaviourPunCallbacks
{

    public Text ServerName;
    public static ServerLauncher s_instance;


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
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();

    }
    public void JoinRoom(Text roomname)
    {
        PhotonNetwork.JoinRoom(roomname.text);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("server connet");
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("lobby connet");
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("CreateRoom");

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("room joined");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("test");
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("02. ∑£¥˝ ∑Î ¡¢º” Ω«∆–");

        // ∑Î º”º∫ º≥¡§
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 6;
        ro.PlayerTtl = -1;
        ro.EmptyRoomTtl = 86400000;
        ro.CustomRoomProperties = new Hashtable() { { "MASTER", "TRUE" } };
        // ∑Î¿ª ª˝º∫ > ¿⁄µø ¿‘¿Âµ 
        PhotonNetwork.CreateRoom("room_1", ro);

    }
    public void OnMakeRoom()
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 6;
        ro.CustomRoomProperties = new Hashtable() { { "MASTER", "TRUE" } };
        ro.CustomRoomPropertiesForLobby = new string[] { "MASTER" };
        PhotonNetwork.NickName = "MASTER";
        PhotonNetwork.CreateRoom(ServerName.text, ro);
        Debug.Log("RoomCreated");
    }
}
