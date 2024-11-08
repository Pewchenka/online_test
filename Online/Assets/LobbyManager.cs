using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public void Start()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(0, 11);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    public void start()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 10 });
    }
    public void conect()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("game");
    }
}
