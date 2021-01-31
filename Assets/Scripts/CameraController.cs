using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject target;
    public GameObject dolly;
    public Camera camera;

    [Space(10)]

    public float cameraLag = 0.01f;
    public Vector2 zoomLimits = new Vector2(5,50);

    [Header("Sensitivity")]

    [Range( 0.1f,10)] public float zoomSensitivity = 1;
    [Range(0.1f, 10)] public float rotateSensitivity = 1;
    [Range(0.1f, 10)] public float moveSidewaysSensitivity = 1;
    [Range(0.1f, 10)] public float moveForwardSensitivity = 1;

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void FixedUpdate () {

        UpdateRigLocation();
        UpdateRigRotation();
        UpdateCamera();

	}

    void UpdateRigLocation()
    {

        Vector3 position = target.transform.position;
        Vector3 dollyDirection = Vector3.Normalize(dolly.transform.position - target.transform.position);
        Vector3 lookRight = Vector3.Cross(Vector3.up, dollyDirection);
        position += - Input.GetAxis(KeyboardMap.HORIZONTAL) * Vector3.Normalize(lookRight) * moveSidewaysSensitivity;
        position += Input.GetAxis(KeyboardMap.VERTICAL) * Vector3.Normalize(Vector3.Cross(Vector3.up, lookRight)) * moveForwardSensitivity;
        target.transform.position = position;

    }

    void UpdateRigRotation()
    {
        Vector3 euler = target.transform.rotation.eulerAngles;
        euler.y += -Input.GetAxis(KeyboardMap.ROTATE) * rotateSensitivity;
        target.transform.rotation = Quaternion.Euler(euler);

        Vector3 dollyDirection = Vector3.Normalize(dolly.transform.position - target.transform.position);
        float distance = Vector3.Magnitude(dolly.transform.position - target.transform.position);

        distance -= Input.GetAxis(KeyboardMap.ZOOM) * zoomSensitivity;
        distance = Mathf.Clamp(distance, zoomLimits.x, zoomLimits.y);

        dolly.transform.position = target.transform.position + (distance * dollyDirection);

    }

    void UpdateCamera()
    {

        Transform cameraRig = camera.gameObject.transform;

        cameraRig.position = Vector3.Lerp(cameraRig.position, dolly.transform.position, cameraLag);
        cameraRig.LookAt(target.transform);

    }



}
