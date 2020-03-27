using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    public Material changeMat;
    private CameraGrab cameraGrab;
    private CameraUse cameraUse;
    private GameObject tempObject;
    // Start is called before the first frame update
    void Start()
    {
        cameraGrab = FindObjectOfType<CameraGrab>();
        cameraUse = FindObjectOfType<CameraUse>();
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure the player is grabbing an object
        if (cameraGrab.objectGrabbed)
        {
            //Check which object they are grabbing (Using static strings may not be the best idea)
            if (cameraGrab.objectGrabbed.name == "TestBlock")
            {
                //Grab the component we want this to effect, get the child renderer component, and update the material to the passed in material
                GameObject.Find("MyGrab").GetComponent<Renderer>().material = changeMat;
            }
        }
        //Check if the player is trying to use an object
        if (cameraUse.objectToUse)
        {
            print(cameraUse.objectToUse);
            //Create a holder to shorten the object name (So we don't have to type cameraUse.objectToUse every time)
            tempObject = cameraUse.objectToUse;
            if (tempObject.name == "Radio")
            {
                tempObject.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Audio/GameMusic1");
            }
        }
    }
}
