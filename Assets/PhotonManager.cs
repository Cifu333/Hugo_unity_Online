using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
   public static PhotonManager _PHOTON_MANAGER;

    private void Awake()
    {
        if (_PHOTON_MANAGER != null && _PHOTON_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _PHOTON_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);

            //conecto con servidor
            PhotonConnect();
        }

    }

    private void PhotonConnect()
    {
        //gestion iinicio de partida
        PhotonNetwork.AutomaticallySyncScene=true;

        //me conetco al inicio de martida 
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conexion al servidor correcta");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Salio del juego por:" + cause);
        Application.Quit();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("He accedido al lobby");
    }

    public void CreateRoom(string roomName)
    {
        Debug.Log(roomName);
        PhotonNetwork.CreateRoom(roomName, new RoomOptions {MaxPlayers= 8 }) ;
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }


    public override void OnJoinedRoom()
    {
        Debug.Log("Me he unido a la sala guay " + PhotonNetwork.CurrentRoom.Name + " com:" + PhotonNetwork.CurrentRoom.PlayerCount + " jugador");

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("No se ha unido " + returnCode + " que significa" + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
       if(PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Ingame");
        }
    }
}
