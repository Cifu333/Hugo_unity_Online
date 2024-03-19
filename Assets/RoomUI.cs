using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUI : MonoBehaviour
{
    [SerializeField]
    private Button createButton;

    [SerializeField]
    private Button joinButton;

    [SerializeField]
    private Text createText;

    [SerializeField]
    private Text joinText;

    private void Awake()
    {
        {
            createButton.onClick.AddListener(CreateRoom);
            joinButton.onClick.AddListener(JoinRoom);
        }
    }
    private void CreateRoom()
    {
        PhotonManager._PHOTON_MANAGER.CreateRoom(createText.text.ToString());
    }

    private void JoinRoom()
    {
        PhotonManager._PHOTON_MANAGER.JoinRoom(joinText.text.ToString());
    }

}
