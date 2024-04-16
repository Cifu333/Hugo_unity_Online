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
    [SerializeField] private int race;
    [SerializeField] private Dropdown dropdown;



    private void Awake()
    {
        //Definimos listener
        loginButton.onClick.AddListener(Clicked);
        race = 1;
    }

    private void Clicked()
    {
        ////mando la info al network manager
        Network_Manager._NETWORK_MANAGER.Register(registerText.text.ToString(), passwordText.text.ToString(), race.ToString());

      
    }

    public void GetMenuIndex()
    {
        race = dropdown.value +1;

        Debug.Log(race);

    }

}