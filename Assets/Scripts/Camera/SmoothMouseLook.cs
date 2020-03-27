using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmoothMouseLook : MonoBehaviour
{
    public GameObject player;
    public float sensitivity;
    Vector2 rotation;
    Transform playerTransform;
    Transform cameraTransform;
    [HideInInspector] new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = player.GetComponent<Rigidbody>();
        cameraTransform = GetComponent<Transform>();
        rotation = new Vector2(0, 0);

    }
    void Update()
    {
        //Move the rigidbody the camera is attached to
        Quaternion rotation = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensitivity, Vector3.up);
        rigidbody.MoveRotation(rigidbody.rotation * rotation);
    }

}