using UnityEngine;
using Insight;
using Cysharp.Threading.Tasks;
using Mapbox.Unity.Location;
using System;
using Cysharp.Threading.Tasks.Triggers;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Unity.VisualScripting;
using Mapbox.Examples;

public class LocationManager : MonoBehaviour
{
    /* ------------------------------------------ */

    #region Public-Serializable-Variables

    public static LocationManager instance;
    public ImmediatePositionWithLocationProvider Player;
    public LocationProviderFactory LocationFactory;
    ILocationProvider _location;
    public AbstractMap Map;
    public GameObject MapObj;

    public TimeSpan DelayTime;

    #endregion

    /* ------------------------------------------ */

    #region LifeCycle

    public void Awake()
    {
        instance = this;
    }

    /* ------------------------------------------ */

    private void Start()
    {
        DelayTime = TimeSpan.FromSeconds(25);
    }

    /* ------------------------------------------ */

    private void Update()
    {

    }

    #endregion

    /* ------------------------------------------ */

    #region Client

    public async UniTask MapShare()
    {
        Debug.Log("Share");

        await LocationFactory.StartAsync();
        ClientNetworkManager.instance.Connections["Lobby"].Send(new MsgLocationDataSendRequest()
        {
            StartStation = "Saray",
            PositionX = _location.CurrentLocation.LatitudeLongitude.x,
            PositionY = _location.CurrentLocation.LatitudeLongitude.y,
        });

        Debug.Log(_location.CurrentLocation.LatitudeLongitude.x+" "+            _location.CurrentLocation.LatitudeLongitude.y);

        UIManager.Instance.UIMap.SetPosition(_location.CurrentLocation.LatitudeLongitude);
    }

    /* ------------------------------------------ */

    public async void SetLocationProvider(ILocationProvider location)
    {
        _location = location;
    }

    /* ------------------------------------------ */

    public async void ActShareLocation()
    {
        Debug.Log("Share");
        Map.IsShared=true;
        while (true)
        {
            MapShare().Forget();

            await UniTask.Delay(DelayTime);
        }
    }

    /* ------------------------------------------ */

    public async UniTask GetLocation()
    {
        Map.IsShared = false;

        ClientNetworkManager.instance.Connections[ClientNetworkManager.instance.LobbyName].Send(new MsgLocationDataRequest()
        {
            StartStation = "Saray",
            StopStation = "Istanbul",
            Date = DateTime.Now,
        });


    }

    /* ------------------------------------------ */

    public async void ActGetLocation()
    {
        while (true)
        {
            GetLocation().Forget();

            await UniTask.Delay(DelayTime);
        }
    }

    /* ------------------------------------------ */

    public async UniTask SetMapValues(Vector2d Location)
    {
        Map.Initialize(Location,Map.AbsoluteZoom);
        Map.UpdateMap(Location);
      //  MapObj.GetComponent<InitializeMapWithLocationProvider>().IsLocationProvider = false;
      //   ILocationProvider remoteProvider = LocationProviderFactory.Instance.RemoteLocationProvider;
      // remoteProvider.SetCurrentLocation(Location);
    }

    /* ------------------------------------------ */

    public async void LocationDataResponse(InsightNetworkMessage connection)
    {
        MsgLocationDataResponse message = connection.ReadMessage<MsgLocationDataResponse>();
        await SetMapValues(new Vector2d(message.PositionX, message.PositionY));
    }

    /* ------------------------------------------ */


    public async void LocationDataSendResponse(InsightNetworkMessage connection)
    {
        MsgLocationDataSendResponse message = connection.ReadMessage<MsgLocationDataSendResponse>();
        Debug.Log(message.Result);
    }

    #endregion


}