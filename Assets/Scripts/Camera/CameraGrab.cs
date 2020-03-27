using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGrab : MonoBehaviour
{
    //Get the position passed through where the grabbed object should move to (Should be a child object of the camera)
    public Transform grabPosition;
    //Setup user manipulatable grab parameters
    public string grabKey;
    public int grabDistance;
    public int grabMoveSpeed;
    //Variables to check which object we are grabbing and whether we are hitting them.
    [HideInInspector] public RaycastHit hit;
    [HideInInspector] public GameObject objectGrabbed;

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
            //If so, grab the object
            objectGrabbed = hit.transform.gameObject;
        }
        //Otherwise, drop the object
        else if (Input.GetKeyUp(grabKey))
        {
            //Reset the grabbed object to null
            objectGrabbed = null;
        }
        //If we are currently grabbing an object
        if (objectGrabbed)
        {
            //Move the object towards the grabPosition at grabMoveSpeed
            objectGrabbed.GetComponent<Rigidbody>().velocity = grabMoveSpeed * (grabPosition.position - objectGrabbed.transform.position);
        }
    }
}
