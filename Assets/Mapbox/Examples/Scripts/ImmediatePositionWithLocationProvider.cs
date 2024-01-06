namespace Mapbox.Examples
{
	using Mapbox.Unity.Location;
	using Mapbox.Unity.Map;
    using Mapbox.Utils;
    using UnityEngine;

	public class ImmediatePositionWithLocationProvider : MonoBehaviour
	{

		public bool _isInitialized;
		public bool _isShared;


        ILocationProvider _locationProvider;
		ILocationProvider LocationProvider
		{
			get
			{
				if (_locationProvider == null)
				{
					_locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
				}

				return _locationProvider;
			}
		}

		Vector3 _targetPosition;

		void Start()
		{
			_isShared = LocationProviderFactory.Instance.mapManager.IsShared;
			if (_isShared)
			{
                LocationProviderFactory.Instance.mapManager.OnInitialized += () => _isInitialized = true;
            }
			
        }
	
		void LateUpdate()
		{
			if (_isInitialized)
			{
				var map = LocationProviderFactory.Instance.mapManager;
				transform.localPosition = map.GeoToWorldPosition(LocationProvider.CurrentLocation.LatitudeLongitude);
			}
			else if(!_isShared)
			{
                var map = LocationProviderFactory.Instance.mapManager;
                transform.localPosition = map.GeoToWorldPosition(map.CurrentLocationLanLon);
            }
		}
	}
}