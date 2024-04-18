using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Xml.Linq;

public class Photon_Manager : MonoBehaviourPunCallbacks
{
    public static Photon_Manager instance;

    private bool isConected = false;

    private string PlayerNickName;
    private int PlayerRaceID;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            //conecto con el servidor
            PhotonConnect();
        }
    }


    private void PhotonConnect()
    {
        //configfuar el inicio de partida
        PhotonNetwork.AutomaticallySyncScene = true;

        //Me conecto al servidor
        PhotonNetwork.ConnectUsingSettings();

        isConected = true;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conexion al servidor realizada correctamente");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("He accedido al lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("He implosionado por: " + cause);
        Application.Quit();
    }

    public void CreateRoom(string nameRoom)
    {
        Debug.Log("Flag create: " + nameRoom);
        PhotonNetwork.CreateRoom(nameRoom, new RoomOptions { MaxPlayers = 2 });
    }

    public void JoinRoom(string nameRoom)
    {
        Debug.Log("Flag join: " + nameRoom);
        PhotonNetwork.JoinRoom(nameRoom);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Me unido a una sala , llamada: " + PhotonNetwork.CurrentRoom.Name + " con: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Mecachis no me he podido conectarme a la sala por el error: " + returnCode + " que significa " + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("InGame");
        }
    }

    public bool GetPhotonConectedServer()
    {
        return isConected;
    }

    public void SetPlayerName(string name)
    {
        PlayerNickName = name;
    }

    public void SetPlayerRaze(int id)
    {
        PlayerRaceID = id;
    }
    public int Raceid()
    {
        return PlayerRaceID;
    }

    public string PlayerName()
    {
        return PlayerNickName;
    }

}
