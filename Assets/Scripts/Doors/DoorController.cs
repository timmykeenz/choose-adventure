using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //Runs when a collision is detected
    private void OnTriggerStay(Collider other)
    {
        //Check that the player is what we are colliding with and not some object
        if (other.tag.Equals("Player") && Input.GetKeyDown(CameraUse.usedKey))
        {
            //Looks for animator in object's children and set trigger
            GetComponentInChildren<Animator>().SetTrigger("OpenClose");
        }
    }
}
