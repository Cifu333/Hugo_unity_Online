using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawPlayer1;

    [SerializeField]
    private GameObject spawPlayer2;

    private void Awake()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("player", spawPlayer1.transform.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("player", spawPlayer2.transform.position, Quaternion.identity);

        }
    }

}
