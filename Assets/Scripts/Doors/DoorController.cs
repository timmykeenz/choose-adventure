using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpen;
    private GameObject nearTo;
    private bool userCanOpen;
    // Start is called before the first frame update
    void Start()
    {
        nearTo = null;
        isOpen = false;
        userCanOpen = true;
    }

    private void Update()
    {
        //If we are near to the player, open/close door
        if (nearTo != null && Input.GetKeyDown(CameraUse.usedKey) && userCanOpen)
        {
            //Toggle the door
            ToggleDoor();
        }
    }

    //Runs when a collision is detected
    private void OnTriggerStay(Collider other)
    {
        //Check that the player is what we are colliding with and not some object
        if (other.tag.Equals("Player") && Input.GetKeyDown(CameraUse.usedKey) && userCanOpen)
        {
            nearTo = other.gameObject;
        }
    }
    //Reset the gameobject on leave
    private void OnTriggerExit(Collider other)
    {
        nearTo = null;
    }
    
    //Setter to update user's power status on door
    public void SetUserCanOpen(bool userCanOpen)
    {
        this.userCanOpen = userCanOpen;
    }
    //Getter to retrieve user's status on door
    public bool GetUserCanOpen()
    {
        return userCanOpen;
    }

    //Public function that toggles the door (Can be used by outside classes
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        //Looks for animator in object's children and set trigger
        GetComponentInChildren<Animator>().SetTrigger("OpenClose");
    }
}
