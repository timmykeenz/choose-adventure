﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGrab : MonoBehaviour
{
    //Get the position passed through where the grabbed object should move to (Should be a child object of the camera)
    public Transform grabPosition;
    //Setup user manipulatable grab parameters
    public string grabKey;
    public int grabDistance;
    public float throwReduction;
    public int grabMoveSpeed;
    [HideInInspector] public static bool isGrabbing = false;
    //Variables to check which object we are grabbing and whether we are hitting them.
    [HideInInspector] public RaycastHit hit;
    [HideInInspector] public static GameObject objectGrabbed;

    //Update runs every frame
    void Update()
    {
        GrabObject();
    }
    //Method for grabbing objects
    public void GrabObject()
    {
        //Check if the grab key is pressed, they are looking at an object, and that the object is grabbable
        if (Input.GetKeyDown(grabKey) && Physics.Raycast(transform.position, transform.forward, out hit, grabDistance) && hit.transform.GetComponent<Rigidbody>())
        {
            //Let the user know that they are grabbing an object
            isGrabbing = true;
            //If so, grab the object
            objectGrabbed = hit.transform.gameObject;
        }
        //Otherwise, drop the object
        else if (Input.GetKeyUp(grabKey))
        {
            //Update to let user know we are no longer grabbing an ojbect
            isGrabbing = false;
            objectGrabbed.GetComponent<Rigidbody>().velocity = objectGrabbed.GetComponent<Rigidbody>().velocity / (objectGrabbed.GetComponent<Rigidbody>().mass * throwReduction);
            //Reset the grabbed object to null
            objectGrabbed = null;
        }
        //If we are currently grabbing an object
        if (objectGrabbed)
        {
            //Move the object towards the grabPosition at grabMoveSpeed
            objectGrabbed.GetComponent<Rigidbody>().velocity = grabMoveSpeed * (grabPosition.position - objectGrabbed.transform.position) / objectGrabbed.GetComponent<Rigidbody>().mass;
        }
    }
}
