using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOnEnd : MonoBehaviour, Child.ChildRiseListener
{
    public Camera cameraToZoom;

    public float turnAlpha = 0.1f;

    bool followOrb;

    private void Awake()
    {
        Manager.Instance.Child.AddChildRiseListener(this);
    }

    private void Update()
    {
        if (followOrb)
            cameraToZoom.transform.rotation = Quaternion.Lerp(cameraToZoom.transform.rotation, Quaternion.LookRotation((Manager.Instance.Child.childRiseInstance.transform.GetChild(0).position - cameraToZoom.transform.position).normalized), turnAlpha);
    }

    public void OnChildRise()
    {
        cameraToZoom.GetComponent<CameraController>().enabled = false;

        StartCoroutine(ZoomIn());
    }

    IEnumerator ZoomIn()
    {
        float fov = cameraToZoom.fieldOfView;
        float max = 400;

        followOrb = true;

        for (int i = 0; i < max; i++)
        {
            float alpha = i / max;
            cameraToZoom.fieldOfView = (1 - alpha) * fov;
            yield return new WaitForSeconds(0.02f);
        }

        followOrb = false;

        Manager.Instance.EndGame(true);
    }
}
