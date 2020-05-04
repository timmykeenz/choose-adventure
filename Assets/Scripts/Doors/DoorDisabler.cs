using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDisabler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Grab all objects that have the door tag
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Door");
        //Loop through each door found
        foreach (GameObject obj in objs)
        {
            obj.GetComponentInChildren<DoorController>().SetUserCanOpen(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Transform>().position.z > 24.55 && other.CompareTag("Player"))
        {
            //Make sure door only closes once
            if (!StoryPilot.roomChosen)
            {
                //Make the decision in the pilot
                StoryPilot.roomChosen = true;
                //Check red door
                if (StoryPilot.redRoomTrigger)
                {
                    StoryPilot.redRefDoor.ToggleDoor();
                }
                //Check blue door
                if (StoryPilot.blueRoomTrigger)
                {
                    StoryPilot.blueRefDoor.ToggleDoor();
                }
            }
        }
        //Grab all objects that have the door tag
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Door");
        //Loop through each door found
        foreach (GameObject obj in objs)
        {
            //Find the component that accesses the doors and enable it
            obj.GetComponentInChildren<DoorController>().SetUserCanOpen(true);
        }
    }
}
