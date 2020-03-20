using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGrab : MonoBehaviour
{
    [HideInInspector] RaycastHit hit;
    GameObject objectGrabbed;
    public Transform grabPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(transform.position, transform.forward, out hit, 5) && hit.transform.GetComponent<Rigidbody>())
        {
            print("Object Grabbed!");
            objectGrabbed = hit.transform.gameObject;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            objectGrabbed = null;
        }

        if (objectGrabbed)
        {
            objectGrabbed.GetComponent<Rigidbody>().velocity = 20 * (grabPosition.position - objectGrabbed.transform.position);
        }
    }
}
