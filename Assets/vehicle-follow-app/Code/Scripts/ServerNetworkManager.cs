using Insight;
using kcp2k;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using Telepathy;
using UnityEngine;

public class ServerNetworkManager : MonoBehaviour
{
    /* ------------------------------------------ */

    #region Public-Serializable-Variables

    public static ServerNetworkManager instance;

    /// <summary>  
    /// Server prefabini tutuyoruz burada  
    /// </summary>
    [Tooltip("Server Prefabýný yerleþtirmelisin yoksa server oluþmaz.")]
    [SerializeField]
    private GameObject _prefab;

    public Dictionary<string, InsightNetworkConnection>
        Connections = new Dictionary<string, InsightNetworkConnection>();

    int Port = 7777;

    #endregion

    /* ------------------------------------------ */

    #region LifeCycle

    private void Awake()
    {
        instance = this;

       /* if (PlayerPrefs.HasKey("CurrentPort"))
        {
            Port = PlayerPrefs.GetInt("CurrentPort") + 1;
            PlayerPrefs.SetInt("CurrentPort", Port);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentPort", Port);
        }*/


        StartServer();
    }

    #endregion

    /* ------------------------------------------ */

    #region Server

    public void StartServer()
    {
        GameObject tempPrefab = Instantiate(_prefab);
        InsightServer tempInsightServer = tempPrefab.GetComponent<InsightServer>();
        TelepathyTransport tempTelepathyTransport = tempInsightServer.GetComponent<TelepathyTransport>();
        tempTelepathyTransport.port = (ushort)Port;
        tempInsightServer.StartInsight();
    }

    #endregion

    /* ------------------------------------------ */
}
