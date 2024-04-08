using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Register_Screen : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Text registerText;
    [SerializeField] private Text passwordText;
    [SerializeField] private string race;
    [SerializeField] private Dropdown dropdown;


    private void Update()
    {
        race = dropdown.itemText.text;
    }

    private void Awake()
    {
        //Definimos listener
        loginButton.onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        ////mando la info al network manager
        //Network_Manager._NETWORK_MANAGER.Register(registerText.text.ToString(), passwordText.text.ToString(), race);

        //if (Network_Manager._NETWORK_MANAGER.GetRegisterInfo())
        //{
        //    Network_Manager._NETWORK_MANAGER.SetRegisterInfo(false);
        //    SceneManager.LoadScene(3);
        //}
        //else
        //{
        //    //Indicar que el usuario ya existe
        //}
    }
}