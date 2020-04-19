using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerStory : MonoBehaviour
{
    public bool doorActive;
    private bool userCanOpen;
    // Start is called before the first frame update
    void Start()
    {
        doorActive = false;
        userCanOpen = true;
    }

    //Runs when a collision is detected
    private void OnTriggerStay(Collider other)
    {
        //Check that the player is what we are colliding with and not some object
        if (other.tag.Equals("Player") && Input.GetKeyDown(CameraUse.usedKey) && userCanOpen)
        {
            //Toggle the door
            ToggleDoor();
        }
    }
    //Setter to update user's power status on door
    public void SetUserCanOpen(bool userCanOpen)
    {
        this.userCanOpen = userCanOpen;
    }
    //Getter to retrieve user's status on door
    public bool GetUserCanOpen()
    {
        return this.userCanOpen;
    }

    //Public function that toggles the door (Can be used by outside classes
    public void ToggleDoor()
    {
        doorActive = !doorActive;
        //Looks for animator in object's children and set trigger
        GetComponentInChildren<Animator>().SetTrigger("OpenClose");
    }
}
