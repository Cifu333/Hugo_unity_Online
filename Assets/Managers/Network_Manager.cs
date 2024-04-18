using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net.Sockets;
using System;

public class Network_Manager : MonoBehaviour
{
    public static Network_Manager _NETWORK_MANAGER;

    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;
    private bool connected = false;

    private bool LoginSuccesful = false;
    private bool RegisterSuccesful = false;

    private string actualPlayer;
    private int actualRace;

    const string host = "10.40.2.158";
    //const int port = 6543;
    const int port = 6227;

    [SerializeField] private whitelist raceslist;

    private void Awake()
    {
        if ( _NETWORK_MANAGER != null && _NETWORK_MANAGER != this)
        { 
       
            Destroy(this.gameObject );
        }
        else
        {
            _NETWORK_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void ManageData(string data)
    {
        string[] parameter = data.Split('/');

        if (parameter[0] == "Ping")
        {
            Debug.Log("Recibo ping");
            writer.WriteLine("1");
            writer.Flush();
        }

        if (parameter[0] == "Ver")
        {
            if(PlayerPrefs.GetString ("Parches","-1")!= parameter[1])
            {
                PlayerPrefs.SetString("Parches", parameter[1]);
                PlayerPrefs.Save ();
                AskRacesInfo();
                Debug.Log("SEXO?");
            }

        }
        if (parameter[0] =="racesData")
        {
            //aki falyan cosas
           // PlayerPrefs.SetInt("RacesData", int.Parse(parameter[1]));
          //  int numberRaces = int.Parse(parameter[1]);
           // Debug.Log (numberRaces);
            //mirar la razas pasar info falta texto 

            raceslist.Races[0].idRace = int.Parse(parameter[1]);
            raceslist.Races[0].name = parameter[2];
            raceslist.Races[0].velocity = float.Parse(parameter[3]);
            raceslist.Races[0].damage = float.Parse(parameter[4]);
            raceslist.Races[0].ratefire = float.Parse(parameter[5]);
            raceslist.Races[0].life = float.Parse(parameter[6]);
            raceslist.Races[0].jump = float.Parse(parameter[7]);

            raceslist.Races[1].idRace = int.Parse(parameter[8]);
            raceslist.Races[1].name = parameter[0];
            raceslist.Races[1].velocity = float.Parse(parameter[10]);
            raceslist.Races[1].damage = float.Parse(parameter[11]);
            raceslist.Races[1].ratefire = float.Parse(parameter[12]);
            raceslist.Races[1].life = float.Parse(parameter[13]);
            raceslist.Races[1].jump = float.Parse(parameter[14]);



        }

        if (parameter[0] == "Register")
        {
            if (parameter[1] == "Invalid")
            {
                RegisterSuccesful = false;
            }
            else if (parameter[1] == "Valid")
            {
                RegisterSuccesful = true;
            }
        }

        if (parameter[0] == "Login")
        {
            if (parameter[1] == "Invalid")
            {
               LoginSuccesful = false;
            }
            else if (parameter[1] == "Valid")
            {
                LoginSuccesful = true;
            }
        }


        if (parameter[0] == "PlayerRaze")
        {
            actualRace = int.Parse(parameter[1]); 
        }

    }

   

    private void Update()
    {
        if ( connected ) 
        {
            if(stream.DataAvailable) 
            {
                string data = reader.ReadLine();
                if ( data != null ) 
                {
                    ManageData(data);
                }
            }
        }
    }

    public void CheckVersion()
    {
        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            connected = true;

            writer.WriteLine("3");
            writer.Flush();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            connected = false;
        }
    }

    public void ChechActualRace()
    {
        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            connected = true;

            writer.WriteLine("5");
            writer.Flush();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            connected = false;
        }
    }
    public void ConnectedToServer(string nick, string password)
    {
        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            connected = true;

            writer.WriteLine("0/" +  nick + "/" +password
                );
            writer.Flush();
                   actualPlayer = nick;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            connected = false;
        }
    }

    public void Register(string nick, string password, string race)
    {
        try 
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            connected = true;

            writer.WriteLine("2/"+ nick + "/" +password+ "/" +race);
            writer.Flush();

        }catch (Exception ex)
        { 
            Debug.LogException(ex);
            connected = false;
        }
    }

    public void AskRacesInfo()
    {
        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            connected = true;

            writer.WriteLine("4");
            writer.Flush();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            connected = false;
        }
    }


    public bool GetLoginData()
    {
        return LoginSuccesful;
    }

    public void SetLoginData()
    {
        LoginSuccesful = false;
    }
    public bool GetRegisterData()
    {
        return RegisterSuccesful;
    }

    public void SetRegisterData()
    {
        RegisterSuccesful = false;
    }

    public whitelist GetRacesList()
    {
        return raceslist;
    }

}
