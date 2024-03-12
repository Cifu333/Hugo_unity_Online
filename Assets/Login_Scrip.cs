using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Login_Script : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private TextMeshProUGUI loginText;
    [SerializeField] private TextMeshProUGUI paswordText;


    private void Awake()
    {
        loginButton.onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        //mandar info al manager
        Network_Manager._NETWORK_MANAGER.ConnectedToServer(loginText.text.ToString(), paswordText.text.ToString());
    }
}
