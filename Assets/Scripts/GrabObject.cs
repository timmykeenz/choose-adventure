using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public Transform player, playerCam;
    public float dropForce, grabDistance;
    [HideInInspector] new Rigidbody rigidbody;
    [HideInInspector] float dist;
    [HideInInspector] static bool isCarrying;
    // Start is called before the first frame update
    void Start()
    {
        //Grab our object's rigidbody
        rigidbody = GetComponent<Rigidbody>();
        //Set variables to false
        isCarrying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanGrab() && Input.GetKeyDown("e") && !isCarrying)
        {
            isCarrying = true;
            //Make physics effects useless
            rigidbody.isKinematic = true;
            //THIS LINE MAY CAUSE ISSUES
            transform.parent = playerCam;
            transform.localPosition = new Vector3(0, 0, 1.5f);
        }
        else if (Input.GetKeyUp("e"))
        {
            isCarrying = false;
            //Allow physics effects and remove parent transform
            rigidbody.isKinematic = false;
            transform.parent = null;
            //Apply player velocity for natural drop
            //rigidbody.AddForce(0,0,3000);
        }
    }

    bool CanGrab()
    {
        //Calculate distance between player and object (gameObject grabs current object)
        dist = Vector3.Distance(gameObject.transform.position, player.position);
        //Return if
        return dist <= grabDistance;
    }
}
