using UnityEngine;
using Insight;
using Cysharp.Threading.Tasks;
using Mapbox.Unity.Location;
using System;
using Cysharp.Threading.Tasks.Triggers;

public class LocationManager : MonoBehaviour
{
    /* ------------------------------------------ */

    #region Public-Serializable-Variables

    public static LocationManager instance;

    public LocationProviderFactory Location;
    public GameObject Map;
    public GameObject MapCanvas;
    public GameObject Canvas;

    public TimeSpan DelayTime;

    #endregion

    /* ------------------------------------------ */

    #region LifeCycle

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DelayTime = TimeSpan.FromSeconds(5);
    }

    #endregion

    /* ------------------------------------------ */

    #region Client

    public async UniTask MapShare()
    {

        Location tempPosition = Location.DeviceLocationProvider.CurrentLocation;
        await Location.StartAsync();
        ClientNetworkManager.instance.Connections["Lobby"].Send(new MapDataSend()
        {
            StartStation = "Saray",

            Position = new Vector2((float)tempPosition.LatitudeLongitude.x, (float)tempPosition.LatitudeLongitude.y)
        });


    }

    /* ------------------------------------------ */

    public async void ShareLocationAction()
    {
        while (true)
        {
            MapShare().Forget();

            await UniTask.Delay(DelayTime);

        }
    }

    /* ------------------------------------------ */

    public async UniTask GetLocation()
    {

        ClientNetworkManager.instance.Connections["Lobby"].Send(new MsgMapDataRequest()
        {

        });


    }

    /* ------------------------------------------ */

    public async void GetLocationAction()
    {
        Map.SetActive(true);
        MapCanvas.SetActive(true);
        Canvas.SetActive(false);
        while (true)
        {
            GetLocation().Forget();

            await UniTask.Delay(DelayTime);

        }
    }

    /* ------------------------------------------ */

    public async void SetMapValues(float Longitute, float Lenght)
    {

    }

    /* ------------------------------------------ */

    public async void MapDataResponse(InsightNetworkMessage connection)
    {
        MsgMapDataResponse message = connection.ReadMessage<MsgMapDataResponse>();
        SetMapValues(message.Position.x, message.Position.y);
    }

    /* ------------------------------------------ */


    public async void MapShareResponse(InsightNetworkMessage connection)
    {
        MsgMapShareResponse message = connection.ReadMessage<MsgMapShareResponse>();
        Debug.Log(message.Result);
    }

    #endregion


}