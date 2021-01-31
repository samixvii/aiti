using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerOverlay : MonoBehaviour
{

    public void StartOverlay()
    {
        gameObject.SetActive(true);
    }

    public void StopOverlay()
    {
        gameObject.SetActive(false);
    }

}
