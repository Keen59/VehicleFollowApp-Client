using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using Insight;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using Cysharp.Threading.Tasks;

public class ClientNetworkManager : MonoBehaviour
{
    /* ------------------------------------------ */

    #region Public-Serializable-Variables

    public static ClientNetworkManager instance;

    public string LobbyName;


    /// <summary>  
    /// Client prefabini tutuyoruz burada  
    /// </summary>
    [Tooltip("Client Prefabýný yerleþtirmelisin yoksa client oluþmaz.")]
    public GameObject Prefab;

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
        test();
    }
    #endregion

    [ContextMenu("test")] //145.239.233.202
    public void test()
    {
        InsightClient client = CreateConnection(LobbyName, "127.0.1.2", port);
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
        Client.ActClientConnected = OnClientConnected;
        Client.ActClientDisconnected = OnClientDisconnected;

        Connections.Add(connectionName, Client);
        return Client;
    }

    #endregion

    /* ------------------------------------------ */

    #region Client
 
    
    public void OnClientConnected()
    {
        Debug.Log("Connectedd");
         Connections[LobbyName].RegisterHandler<MsgMapShareResponse>(LocationManager.instance.MapShareResponse);

        /* Connections[LobbyName].Send(new MsgUserLoginRequest
         {
             CryptedIDs = new[] { "aa", "aa", "aa" },
             IDs = new[] { "kaan", "kaan", "kaan" }
         });*/
        /*   Connections[LobbyName].Send(new MsgUserLoginWithPassRequest()
           {
               DID = "444",
               Password = "444",
               Version = "0.0.3"
           });*/
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