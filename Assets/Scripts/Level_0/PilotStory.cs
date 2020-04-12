using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotStory : MonoBehaviour
{
    public GameObject gate;
    private bool triggerGate = false;
    // Update is called once per frame
    void Update()
    {
        //Check if the object exists
        if (CameraUse.objectToUse && CameraUse.isUsing)
        {
            if (CameraUse.objectToUse.tag.Equals("Radio") && !triggerGate)
            {
                triggerGate = true;
                //Looks for animator in object's children and set trigger
                gate.GetComponentInChildren<Animator>().SetTrigger("OpenClose");
            }
        }
    }
}
