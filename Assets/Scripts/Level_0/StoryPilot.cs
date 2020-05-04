using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPilot : MonoBehaviour
{
    //PUBLIC OBJECTS
    public GameObject gate;
    public GameObject batteryHolderText;
    public GameObject radioHelpText;
    public DoorController redDoor;
    public DoorController blueDoor;
    public GameObject blueChoiceRoom;
    public GameObject redChoiceRoom;
    public float roomDeactivationTime;
    public AudioSource audioSource;
    public AudioSource radioSource;
    public AudioSource radioRed;
    public AudioSource radioBlue;
    //LOCAL VARIABLES
    private bool startRadio;
    private bool doorsEnabled;
    private bool loadFullMusic;
    private bool playRoomChoice;
    //STATIC ELEMENTS (Used in tracking outside Scripts)
    [HideInInspector] public static DoorController redRefDoor;
    [HideInInspector] public static DoorController blueRefDoor;
    [HideInInspector] public static bool redRoomTrigger;
    [HideInInspector] public static bool blueRoomTrigger;
    [HideInInspector] public static bool roomChosen;
    public static bool entryGateOpened;
    public static bool hasGrabbedBattery;
    private static bool showHolderText;
    //Song switching variables
    private static string songURL;

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
        //Music will not be fully loaded at start
        loadFullMusic = false;
        //Song URL is empty be default
        songURL = "";
        //Bool to make sure audio is only played once
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
                //Check to make sure radio audio is not playing
                if (!radioSource.isPlaying)
                {
                    //Check which door the user chose 
                    CheckDoors();
                }
            } else
            {
                if (!loadFullMusic)
                {
                    loadFullMusic = true;
                    songURL = "Audio/Level0/Level0BG_Full";
                }
                // See if the final audio clip has been played
                if (!playRoomChoice)
                {
                    playRoomChoice = true;
                    //If not, check if the red room was chosen
                    if (redRoomTrigger)
                    {
                        //If so, play the clip
                        radioRed.clip = (AudioClip)Resources.Load("Audio/Level0/RadioAudioRed");
                        radioRed.Play();
                    }
                    //Check if the blue room was chosen
                    if (blueRoomTrigger)
                    {
                        //If so, play the right clip
                        radioBlue.clip = (AudioClip)Resources.Load("Audio/Level0/RadioAudioBlue");
                        radioBlue.Play();
                    }

                }
                //Check if the player is trying to use an object
                if (CameraUse.objectToUse)
                {
                    if (CameraUse.objectToUse.CompareTag("Book") && CameraUse.isUsing)
                    {
                        //Load Menu
                        Levels levelSelector = gameObject.AddComponent<Levels>();
                        levelSelector.LoadMenu();
                    }
                }
            }
        }
        CheckSong();
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
            songURL = "Audio/Level0/Level0BG_Mixed";
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
            if (CameraGrab.objectGrabbed.CompareTag("Battery") && !hasGrabbedBattery)
            {
                hasGrabbedBattery = true;
                //Check if we have a tutorial element
                if (GameObject.Find("GrabHelperText").CompareTag("Tutorial"))
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
            if (CameraUse.objectToUse.CompareTag("Radio") && CameraUse.isUsing)
            {
                if (!startRadio)
                {
                    GameObject.FindGameObjectWithTag("Radio").GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Audio/Level0/RadioAudio");
                    GameObject.FindGameObjectWithTag("Radio").GetComponent<AudioSource>().Play();
                    startRadio = true;
                    //Run this on radio use to remove help text
                    radioHelpText.SetActive(false);
                } else
                {
                    if (radioSource.isPlaying)
                    {
                        radioSource.Stop();
                    }
                    if (radioBlue.isPlaying)
                    {
                        radioBlue.Stop();
                    }
                    if (radioRed.isPlaying)
                    {
                        radioRed.Stop();
                    }
                }
            }
        }
    }
    private void CheckSong()
    {
        //Check to make sure string is not empty
        if (!songURL.Equals(""))
        {
            //Start coroutine to change song on next beat change
            StartCoroutine(ChangeSong(AudioSync.GetTime(), songURL));
            //Reset triggers
            songURL = "";
        }
    }
    IEnumerator ChangeSong(float time, string clipURL)
    {
        //Make code wait for 'x' amount of time before executing below
        yield return new WaitForSeconds(time);
        //Change the song
        AudioSync.ChangeSong(clipURL);
    }
}
