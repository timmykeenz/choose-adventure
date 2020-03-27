using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    public Material changeMat;
    private CameraGrab cameraGrab;
    private CameraUse cameraUse;
    private GameObject tempObject;
    private bool removeTutorialText;
    // Start is called before the first frame update
    void Start()
    {
        cameraGrab = FindObjectOfType<CameraGrab>();
        cameraUse = FindObjectOfType<CameraUse>();
        removeTutorialText = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if we are grabbing the radio
        CheckRadioGrab();
        //Check if we are using the radio
        CheckRadioUse();
    }
    public void CheckRadioGrab()
    {
        //Check if player is grabbing an object
        if (cameraGrab.objectGrabbed)
        {
            //Check which object they are grabbing (Using static strings may not be the best idea)
            if (cameraGrab.objectGrabbed.name == "Radio")
            {
                removeTutorialText = true;
            }
        }
    }

    public void CheckRadioUse()
    {
        //Check if the player is trying to use an object
        if (cameraUse.objectToUse)
        {
            //Create a holder to shorten the object name (So we don't have to type cameraUse.objectToUse every time)
            tempObject = cameraUse.objectToUse;
            if (tempObject.name == "Radio")
            {
                //Load in an audio clip to the radio
                tempObject.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Audio/GameMusic2");
                //Activate the audio source
                if (Camera.main.GetComponent<CameraUse>().isUsing)
                {
                    //Activate and play our music in our Radio's script
                    tempObject.GetComponent<PlayMusic>().activate = true;
                }
                //Reset in case the object is used again
                else
                {
                    tempObject.GetComponent<PlayMusic>().activate = false;
                }
                //Change the tutorial text
                if (!removeTutorialText)
                {
                    tempObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshPro>().text = "Press 'e' to grab me!";
                    //Once they've grabbed the object, remove the text
                }
                else
                {
                    //Remove the tutorial text
                    tempObject.transform.GetChild(0).gameObject.GetComponent<DisplayInRange>().displayText = false;
                }
            }
        }
    }
}
