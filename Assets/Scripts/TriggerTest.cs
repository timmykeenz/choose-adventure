using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    public Material changeMat;
    private CameraGrab cameraScript;
    // Start is called before the first frame update
    void Start()
    {
        cameraScript = FindObjectOfType<CameraGrab>();
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure the player is grabbing an object
        if (cameraScript.objectGrabbed)
        {
            //Check which object they are grabbing (Using static strings may not be the best idea)
            if (cameraScript.objectGrabbed.name == "TestBlock")
            {
                //Grab the component we want this to effect, get the child renderer component, and update the material to the passed in material
                GameObject.Find("MyGrab").GetComponent<Renderer>().material = changeMat;
            }
        }
    }
}
