using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMapGroup : MonoBehaviour
{
    public TMP_Text TxtLocation;
    
    public void SetPosition(Vector2d Location)
    {
        TxtLocation.text = "Konum Paylaþýlýyor konumunuz: x" + Location.x + " y:" + Location.y;
    }
}
