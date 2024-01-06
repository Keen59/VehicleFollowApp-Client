using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using Insight;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using Cysharp.Threading.Tasks;
using System;

public class ClientNetworkManager : MonoBehaviour
{
    /* ------------------------------------------ */

    #region Public-Serializable-Variables

    public static ClientNetworkManager instance;

    public string LobbyName;

    public UINetworkGroup UINetworkGroup;
    
    /// <summary>  
    /// Client prefabini tutuyoruz burada  
    /// </summary>
    [Tooltip("Client Prefabýný yerleþtirmelisin yoksa client oluþmaz.")]
    public GameObject Prefab;

    public string NetworkAdress;
    public ushort port;

    /// <summary>  
    /// Baðlantýlarý burada tutuyoruz. 
    /// </summary>
    [Tooltip("Inspectorden herhangi bir yerleþtirme yapmana gerek yok.")]
    public Dictionary<string, InsightClient> Connections = new Dictionary<string, InsightClient>();

    #endregion

    /* ------------------------------------------ */

    #region LifeCycle

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Connect();
    }
    #endregion

    [ContextMenu("test")] //145.239.233.202//3.77.201.144
    public void Connect()
    {
        InsightClient client = CreateConnection(LobbyName, NetworkAdress, port);
        client.StartInsight();
    }

    /* ------------------------------------------ */

    #region Server

    public InsightClient CreateConnection(string connectionName, string networkAddress, ushort port)
    {
        GameObject tempClient = Instantiate(Prefab);
        InsightClient Client = tempClient.GetComponent<InsightClient>();
        Client.networkAddress = networkAddress;

        TelepathyTransport transport = Client.GetComponent<TelepathyTransport>();
        transport.port = port;
        Client.ActClientConnected = OnClientConnect;
        Client.ActClientDisconnected = OnClientDisconnected;

        Connections.Add(connectionName, Client);
        return Client;
    }

    #endregion

    /* ------------------------------------------ */

    #region Client


    public void OnClientConnect()
    {
        Debug.Log("Connectedd");
        Connections[LobbyName].RegisterHandler<MsgLocationDataSendResponse>(LocationManager.instance.LocationDataSendResponse);
        Connections[LobbyName].RegisterHandler<MsgLocationDataResponse>(LocationManager.instance.LocationDataResponse);
        UINetworkGroup.Initialize();
    }

    /* ------------------------------------------ */

    public void OnClientDisconnected(InsightNetworkConnection Client)
    {
        /*  foreach (var key in ServerNetworkManager.instance.Connections.Keys.ToList())
          {
              if (FootballMatesServerNetworkManager.instance.Connections[key] == Client)
              {
                  FootballMatesServerNetworkManager.instance.Connections.Remove(key);
                  break; // Deðeri bulduktan sonra döngüden çýkabiliriz.
              }
          }*/
    }

    /* ------------------------------------------ */


    #endregion

    /* ------------------------------------------ */

}