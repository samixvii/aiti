using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public new Camera camera;
    public Transform stillPoint;

    float fov;

    private void Start()
    {
        fov = camera.fieldOfView;
    }

    public void GoToStill()
    {
        camera.transform.position = stillPoint.transform.position;
        camera.transform.rotation = stillPoint.transform.rotation;
        camera.GetComponent<CameraController>().enabled = false;
        camera.fieldOfView = fov;
    }

}
