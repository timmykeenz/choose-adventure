using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    public Material changeMat;
    public GameObject player;
    private CameraGrab cameraGrab;
    private CameraUse cameraUse;
    private GameObject tempObject;
    private GameObject chairController;
    private AudioSource radioAudio;
    private float delayTime;
    private bool chairFlag;
    private bool removeTutorialText;
    // Start is called before the first frame update
    void Start()
    {
        cameraGrab = FindObjectOfType<CameraGrab>();
        cameraUse = FindObjectOfType<CameraUse>();
        removeTutorialText = false;
        //Setup chair part of sequence
        chairController = GameObject.Find("ChairController");
        chairController.SetActive(false);
        radioAudio = GameObject.Find("Radio").GetComponent<AudioSource>();
        chairFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if we are grabbing the radio
        CheckRadioGrab();
        //Check if we are using the radio
        CheckRadioUse();
        //Check chair distance
        CheckChairDistance();
    }
    public void CheckChairDistance()
    {
        //Only run this method if the parent container is active
        if (chairController.activeSelf)
        {
            GameObject armChairBlue = GameObject.Find("ArmChairBlue");
            GameObject armChairRed = GameObject.Find("ArmChairRed");
            Light chairLight = GameObject.Find("ChairLight").GetComponent<Light>();

            //Calculate distance from object to player
            double distanceBlue = Vector3.Distance(player.GetComponent<Transform>().transform.position, armChairBlue.GetComponent<Transform>().transform.position);
            double distanceRed = Vector3.Distance(player.GetComponent<Transform>().transform.position, armChairRed.GetComponent<Transform>().transform.position);
            //Make sure the radio isn't playing
            if (!radioAudio.isPlaying)
            {
                //See if they are close to the blue chair
                if (distanceBlue < 1.5 && !chairFlag)
                {
                    chairLight.color = UnityEngine.Color.blue;
                    radioAudio.clip = (AudioClip)Resources.Load("Audio/TestLevel/TestLevelGood");
                    GameObject.Find("Radio").GetComponent<PlayMusic>().activate = false;
                    GameObject.Find("Radio").GetComponent<PlayMusic>().activate = true;
                    chairFlag = true;
                    delayTime = 7f;
                }
                //See if they are close to the red chair
                else if (distanceRed < 1.5 && !chairFlag)
                {
                    chairLight.color = UnityEngine.Color.red;
                    radioAudio.clip = (AudioClip)Resources.Load("Audio/TestLevel/TestLevelBad");
                    GameObject.Find("Radio").GetComponent<PlayMusic>().activate = false;
                    GameObject.Find("Radio").GetComponent<PlayMusic>().activate = true;
                    delayTime = 14f;
                    chairFlag = true;
                }
            }
            //Throw the chair if we've done our task
            if (chairFlag && radioAudio.isPlaying)
            {
                //Get rid of that red chair after x seconds
                StartCoroutine(ShootArmChair(armChairRed, delayTime));
            }
        }
    }
    //IEnumerator is for delayed functions
    IEnumerator ShootArmChair(GameObject armChairRed, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //Applay the force
        armChairRed.GetComponent<Rigidbody>().AddForce(-1000, 0, 0);
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
                //Activate the audio source
                if (Camera.main.GetComponent<CameraUse>().isUsing)
                {
                    if (!chairFlag)
                    {
                        //Load in an audio clip to the radio
                        radioAudio.clip = (AudioClip)Resources.Load("Audio/TestLevel/TestLevelIntro");
                    }
                    //Activate and play our music in our Radio's script
                    tempObject.GetComponent<PlayMusic>().activate = true;
                    //Check if the chairs are active
                    if (!chairController.activeSelf) {
                        chairController.SetActive(true);
                    }
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
