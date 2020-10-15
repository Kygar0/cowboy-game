using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class LobbyController : MonoBehaviourPunCallbacks
{
    public int roomSize;

    public void JOinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        print("trying to join");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("can't find room");
        CreateRoom();

    }
    void CreateRoom()
    {
        print("creating room");
        int RandomRoomNumber = Random.Range(0,100000);
        print(RandomRoomNumber);

        RoomOptions options = new RoomOptions
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)roomSize

        };
        PhotonNetwork.CreateRoom("room" + RandomRoomNumber, options);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public void cancel()
    {
        PhotonNetwork.LeaveRoom();
    }
}
