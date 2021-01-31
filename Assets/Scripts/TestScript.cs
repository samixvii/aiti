using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour, Placer.Placable
{
    public GameObject placePreview;

    public void OnPlaced(Vector3 placedLocation)
    {
        Debug.Log("Placed " + placedLocation);
    }

    public void OnTryPlaced(Vector3 placedLocation)
    {

    }

    private void Start()
    {

    }

}
