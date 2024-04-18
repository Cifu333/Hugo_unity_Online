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
        Photon_Manager.instance.CreateRoom(createText.text.ToString());
    }

    private void JoinRoom()
    {
        Photon_Manager.instance.JoinRoom(joinText.text.ToString());
    }

}
