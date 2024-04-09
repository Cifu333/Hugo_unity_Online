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

    const string host = "10.40.2.158";
    //const int port = 6543;
    const int port = 6227;

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
            }

        }
        if (parameter[0] =="racesData")
        {
            //aki falyan cosas
            PlayerPrefs.SetInt("RacesData", int.Parse(parameter[1]));
            int numberRaces = int.Parse(parameter[1]);
            Debug.Log (numberRaces);
            //mirar la razas pasar info falta texto 
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

    public void ConnectedToServer(string nick, string password)
    {
        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            connected = true;

            writer.WriteLine("0/" +  nick + "/" +password);
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

}
