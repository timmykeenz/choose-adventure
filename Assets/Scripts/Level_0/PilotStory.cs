using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotStory : MonoBehaviour
{
    private bool explodedWalls = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the object exists
        if (CameraUse.objectToUse && CameraUse.isUsing && !explodedWalls)
        {
            if (CameraUse.objectToUse.tag.Equals("Radio"))
            {
                explodedWalls = true;
                GameObject[] walls = GameObject.FindGameObjectsWithTag("ExplodingWall");
                //Loop through walls
                for (int i = 0; i < walls.Length; i++)
                {
                    walls[i].GetComponent<Rigidbody>().isKinematic = false;
                    walls[i].GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1000);
                }
            }
        }
    }
}
