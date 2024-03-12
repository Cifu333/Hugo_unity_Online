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

    const string host = "10.40.1.159";
    const int port = 6543;

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
        if(data == "Ping")
        {
            Debug.Log("Recibo ping");
            writer.WriteLine("1");
            writer.Flush();
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
}
