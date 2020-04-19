using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPilot : MonoBehaviour
{
    public GameObject gate;
    public GameObject batteryHolderText;
    public GameObject radioHelpText;
    public DoorController redDoor;
    public DoorController blueDoor;
    public static bool entryGateOpened;
    public static bool hasGrabbedBattery;
    private static bool showHolderText;

    private void Start()
    {
        entryGateOpened = false;
        hasGrabbedBattery = false;
        showHolderText = false;
    }

    void Update()
    {
        //Check if the battery has been plugged in 
        CheckBattery();
        //Check if radio is being used
        CheckRadioUse();
        //Check which door the user chose
        CheckDoors();
    }
    private void CheckDoors()
    {
        if (blueDoor.isOpen)
        {
            redDoor.SetUserCanOpen(false);
        } else
        {
            redDoor.SetUserCanOpen(true);
        }
        if (redDoor.isOpen)
        {
            blueDoor.SetUserCanOpen(false);
        } else
        {
            blueDoor.SetUserCanOpen(true);
        }
    }
    private void CheckBattery()
    {
        if (PlaceBattery.isConnected && !entryGateOpened)
        {
            showHolderText = false;
            entryGateOpened = true;
            gate.GetComponentInChildren<Animator>().Play("OpenGate");
        }
        //Check if we should show the text for the battery holder
        if (showHolderText)
        {
            batteryHolderText.SetActive(true);
            //If not, make it invisible
        }
        else
        {
            batteryHolderText.SetActive(false);
        }

        //Check if we are grabbing an object
        if (CameraGrab.isGrabbing)
        {
            if (!PlaceBattery.isConnected)
            {
                showHolderText = true;
            }
            //Check if the user has grabbed a battery yet
            if (CameraGrab.objectGrabbed.tag.Equals("Battery") && !hasGrabbedBattery)
            {
                hasGrabbedBattery = true;
                //Check if we have a tutorial element
                if (GameObject.Find("GrabHelperText").tag.Equals("Tutorial"))
                {
                    //If so remove the text
                    GameObject.Find("GrabHelperText").SetActive(false);
                }
            }
        }
    }
    private void CheckRadioUse()
    {
        //Check if the player is trying to use an object
        if (CameraUse.objectToUse)
        {
            if (CameraUse.objectToUse.tag.Equals("Radio") && CameraUse.isUsing)
            {
                //Run this on radio use
                radioHelpText.SetActive(false);
            }
        }
    }
}
