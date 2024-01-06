using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINetworkGroup : MonoBehaviour
{
    public GameObject UIMenuGroup;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize()
    {
        UIMenuGroup.SetActive(true);
        gameObject.SetActive(false);
    }

}
