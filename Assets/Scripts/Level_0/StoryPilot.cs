using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPilot : MonoBehaviour
{
    public GameObject gate;
    public static bool entryGateOpened = false;
    public static bool hasGrabbedBattery = false;

    void Update()
    {
        if (PlaceBattery.isConnected && !entryGateOpened)
        {
            entryGateOpened = true;
            gate.GetComponentInChildren<Animator>().Play("OpenGate");
        }

        //Check if we are grabbing an object
        if (CameraGrab.isGrabbing)
        {
            //Check if the user has grabbed a battery yet
            if (CameraGrab.objectGrabbed.tag.Equals("Battery") && !hasGrabbedBattery)
            {
                hasGrabbedBattery = true;
                //Check if we have a tutorial element
                if (GameObject.Find("GrabHelperText").tag.Equals("Tutorial")) {
                    //If so remove the text
                    GameObject.Find("GrabHelperText").SetActive(false);
                }
            }
        }
    }
}
