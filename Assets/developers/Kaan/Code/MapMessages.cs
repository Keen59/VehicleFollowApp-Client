using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MsgLocationDataRequest : NetworkMessage
{
    public string StopStation;
    public string StartStation;
    public DateTime Date;
}
public struct MsgLocationDataResponse : NetworkMessage
{
    public double PositionX;
    public double PositionY;
}

public struct MsgLocationDataSendRequest : NetworkMessage
{
    public string StopStation;
    public string StartStation;
    public DateTime Date;
    public double PositionX;
    public double PositionY;
}
public struct MsgLocationDataSendResponse : NetworkMessage
{
    public string Result;
}