using Mapbox.Unity.Map;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuGroup : MonoBehaviour
{
    public GameObject Map;
    public TMP_Dropdown StartStation;
    public TMP_Dropdown StopStation;
    public Button LocationShareButton;
    public Button GetLocationButton;
    public GameObject UICamera;
    public GameObject UIMap;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialize()
    {
        LocationShareButton.onClick.AddListener(ActShareLocation);
        GetLocationButton.onClick.AddListener(ActGetLocation);
    }
    private void InjectShareMapGameObjects()
    {
        gameObject.SetActive(false);
        Map.SetActive(true);
        UICamera.SetActive(false);
        UIMap.SetActive(true);

    }
    private void InjectGetMapGameObjects()
    {
        gameObject.SetActive(false);
        Map.SetActive(true);
        Map.transform.GetChild(0).GetComponent<InitializeMapWithLocationProvider>().enabled = false;
        UICamera.SetActive(false);
        UIMap.SetActive(true);

    }
    public void ActShareLocation()
    {
        InjectShareMapGameObjects();
        LocationManager.instance.ActShareLocation();
    }
    public void ActGetLocation()
    {
        InjectGetMapGameObjects();
        LocationManager.instance.ActGetLocation();
    }

}
