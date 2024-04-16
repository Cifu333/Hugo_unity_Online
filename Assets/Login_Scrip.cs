using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login_Script : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Text loginText;
    [SerializeField] private Text paswordText;


    private void Awake()
    {
        loginButton.onClick.AddListener(Clicked);

        Network_Manager._NETWORK_MANAGER.CheckVersion();

    }

    private void Update()
    {
        if(Network_Manager._NETWORK_MANAGER.GetLoginData())
        {
            SceneManager.LoadScene("Game");
        }
    }

    private void Clicked()
    {
        //mandar info al manager
        Network_Manager._NETWORK_MANAGER.ConnectedToServer(loginText.text.ToString(), paswordText.text.ToString());
    }
}
