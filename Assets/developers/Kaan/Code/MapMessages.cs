using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MsgMapDataRequest : NetworkMessage
{
  /*  public string StopStation;
    public string StartStation;
    public DateTime Date;*/
}
public struct MapDataSend : NetworkMessage
{
    public string StopStation;
    public string StartStation;
    public DateTime Date;
    public Vector2 Position;
}
public struct MsgMapDataResponse : NetworkMessage
{
    public Vector2 Position;
}
public struct MsgMapShareResponse : NetworkMessage
{
    public string Result;
}