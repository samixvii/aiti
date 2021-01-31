using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCamera : MonoBehaviour
{
    public Transform source;
    public float distance;

    void Update()
    {
        Vector3 location = source.position + ((source.position - Camera.main.transform.position).normalized * distance);
        transform.position = location;

        Quaternion rotation = Quaternion.LookRotation((Camera.main.transform.position - location), Vector3.up);
        transform.rotation = rotation;
    }
}
