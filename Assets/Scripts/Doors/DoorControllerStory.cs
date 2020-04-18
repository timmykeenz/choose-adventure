using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerStory : MonoBehaviour
{
    private bool doorActive;
    // Start is called before the first frame update
    void Start()
    {
        doorActive = false;
    }

    //Runs when a collision is detected
    private void OnTriggerStay(Collider other)
    {
        //Check that the player is what we are colliding with and not some object
        if (other.tag.Equals("Player") && Input.GetKeyDown(CameraUse.usedKey))
        {
            doorActive = !doorActive;
            //Looks for animator in object's children and set trigger
            GetComponentInChildren<Animator>().SetTrigger("OpenClose");
        }
    }
}
