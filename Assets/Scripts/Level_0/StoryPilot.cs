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
    [HideInInspector] public static DoorController redRefDoor;
    [HideInInspector] public static DoorController blueRefDoor;
    public GameObject blueChoiceRoom;
    public GameObject redChoiceRoom;
    public float roomDeactivationTime;
    [HideInInspector] public static bool redRoomTrigger;
    [HideInInspector] public static bool blueRoomTrigger;
    [HideInInspector] public static bool roomChosen;
    public static bool entryGateOpened;
    public static bool hasGrabbedBattery;
    private static bool showHolderText;
    private bool startRadio;
    private bool doorsEnabled;

    private void Start()
    {
        //Disable gate at level open
        entryGateOpened = false;
        //Disable battery grabbing so help text shows
        hasGrabbedBattery = false;
        //Show holder text starts off by default
        showHolderText = false;
        //Radio has not played
        startRadio = false;
        //Doors are disabled by default
        doorsEnabled = false;
        //Doors cannot be opened by default
        redDoor.SetUserCanOpen(false);
        blueDoor.SetUserCanOpen(false);
        //Disable rooms behind doors
        blueChoiceRoom.SetActive(false);
        redChoiceRoom.SetActive(false);
        //Setup triggers for the rooms (Don't touch these values)
        redRoomTrigger = false;
        blueRoomTrigger = false;
        //Make sure the user has not chosen a room on level boot
        roomChosen = false;
        //Setup static references so other objects can utilize the door choices
        redRefDoor = redDoor;
        blueRefDoor = blueDoor;
    }

    void Update()
    {
        //Check if the battery has been plugged in 
        CheckBattery();
        //Check if radio is being used
        CheckRadioUse();
        //Check to make sure user has played radio before making choice
        if (startRadio)
        {
            if (!roomChosen)
            {
                //Check which door the user chose 
                CheckDoors();
            } else
            {
                //---- Use redRoomTrigger and blueRoomTrigger to determine which path the user took ----
                //Check if the player is trying to use an object
                if (CameraUse.objectToUse)
                {
                    if (CameraUse.objectToUse.tag.Equals("Book") && CameraUse.isUsing)
                    {
                        //Load Menu
                        Levels levelSelector = new Levels();
                        levelSelector.LoadMenu();
                    }
                }
            }
        }
    }
    private void CheckDoors()
    { 
        //If blue door is open
        if (blueDoor.isOpen)
        {
            //Disable red door and activate the room
            redDoor.SetUserCanOpen(false);
            blueChoiceRoom.SetActive(true);
            //Setup trigger for deactivation
            blueRoomTrigger = true;
        } else if (blueRoomTrigger)
        {
            //Enable red door and deactivate the room on door close
            redDoor.SetUserCanOpen(true);
            StartCoroutine(DisableGameObject(roomDeactivationTime, blueChoiceRoom));
            //Reset trigger
            blueRoomTrigger = false;
        }
        //If red door is open
        if (redDoor.isOpen)
        {
            //Disable blue door and activate the room
            blueDoor.SetUserCanOpen(false);
            redChoiceRoom.SetActive(true);
            //Setup trigger for deactivation
            redRoomTrigger = true;
        } else if (redRoomTrigger)
        {
            //Enable blue door and deactivate the room on door close
            blueDoor.SetUserCanOpen(true);
            StartCoroutine(DisableGameObject(roomDeactivationTime, redChoiceRoom));
            //Reset trigger
            redRoomTrigger = false;
        } 
        //Enable doors on first iteration
        if (!doorsEnabled)
        {
            doorsEnabled = !doorsEnabled;
            blueDoor.SetUserCanOpen(true);
            redDoor.SetUserCanOpen(true);
        }
    }
    IEnumerator DisableGameObject(float time, GameObject obj)
    {
        //Make code wait for 'x' amount of time before executing below
        yield return new WaitForSeconds(time);
        //Disable GameObject
        obj.SetActive(false);
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
                startRadio = true;
                //Run this on radio use
                radioHelpText.SetActive(false);
            }
        }
    }
}
