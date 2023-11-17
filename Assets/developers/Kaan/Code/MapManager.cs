using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using Insight;
using System;

public class MapManager : MonoBehaviour
{
    /* ------------------------------------------ */

    #region Public-Serializable-Variables

    public static MapManager instance;

    public string StartStation;

    public string StopStation;

    public DateTime Date;

    public Vector2 Position;

    #endregion

    /* ------------------------------------------ */

    #region LifeCycle

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InsightServer.instance.RegisterHandler<MsgMapDataRequest>(MapDataRequest);
        InsightServer.instance.RegisterHandler<MapDataSend>(MapShareRequest);
    }
    #endregion

    /* ------------------------------------------ */

    #region Server

    private async void MapDataRequest(InsightNetworkMessage connection)
    {
        MsgMapDataRequest message = connection.ReadMessage<MsgMapDataRequest>();

        connection.Reply(new MsgMapDataResponse { Position=Position}
        );
    }
    private async void MapShareRequest(InsightNetworkMessage connection)
    {
        MapDataSend message = connection.ReadMessage<MapDataSend>();

        StartStation = message.StartStation;

        StopStation = message.StopStation;

        Date = message.Date;

        Position = message.Position;

        connection.Reply(new MsgMapShareResponse
        {
           Result = "Konum paylaþýlýyor..."
        });
    }

    #endregion

  
}