using UnityEngine;

public class CameraGrab : MonoBehaviour
{
    //Get the position passed through where the grabbed object should move to (Should be a child object of the camera)
    public Transform grabPosition;
    public Transform grabLookPosition;
    //How fast a given item should rotate towards a player
    public float rotSpeed;
    //Setup user manipulatable grab parameters
    public string grabKey;
    public int grabDistance;
    public float throwReduction;
    public int grabMoveSpeed;
    //Variables to check which object we are grabbing and whether we are hitting them.
    [HideInInspector] public RaycastHit hit;
    //These variables are static so other classes can retrieve information on the object grabbed
    [HideInInspector] public static bool isGrabbing = false;
    [HideInInspector] public static GameObject objectGrabbed;

    //Update runs every frame
    void Update()
    {
        GrabObject();
    }
    //FixedUpdate runs a variable amount of times for physics calculations
    private void FixedUpdate()
    {
        //Only update when grabbing an object
        if (isGrabbing)
        {
            //Move the object towards the grabPosition at grabMoveSpeed
            objectGrabbed.GetComponent<Rigidbody>().velocity = grabMoveSpeed * (grabPosition.position - objectGrabbed.transform.position) / objectGrabbed.GetComponent<Rigidbody>().mass;
            //Reduce that angular velocity to mitigate shake when carrying objects
            objectGrabbed.GetComponent<Rigidbody>().angularVelocity = new Vector3(0,0,0);
            //Throw object when released
            if (Input.GetKeyUp(grabKey))
            {
                objectGrabbed.GetComponent<Rigidbody>().velocity = objectGrabbed.GetComponent<Rigidbody>().velocity / (objectGrabbed.GetComponent<Rigidbody>().mass * throwReduction);
            }
        }
    }
    /**
     * Function determines if object can be grabbed.  If so, it will update
     * properties that will make FixedUpdate() run for physics calculations.
     */
    public void GrabObject()
    {
        //Check if the grab key is pressed, they are looking at an object, and that the object is grabbable
        if (Input.GetKeyDown(grabKey) && Physics.Raycast(transform.position, transform.forward, out hit, grabDistance) && hit.transform.GetComponent<Rigidbody>() && !hit.transform.GetComponent<Rigidbody>().isKinematic)
        {
            //Let the user know that they are grabbing an object
            isGrabbing = true;
            //If so, grab the object
            objectGrabbed = hit.transform.gameObject;
        }
        //Otherwise, drop the object
        else if (Input.GetKeyUp(grabKey) || !isGrabbing)
        {
            //Update to let user know we are no longer grabbing an ojbect
            isGrabbing = false;
            //Reset the grabbed object to null
            objectGrabbed = null;
        }
        //If we are currently grabbing an object
        if (objectGrabbed)
        {
            //--- Physics related properties when object is grabbed are located in FixedUpdate ---

            //Rotate the object towards player
            RotateTowardsPlayer();
        }
    }
    /**
     * Function rotates picked up object to look at player
     */
    private void RotateTowardsPlayer()
    {
        //Grab the point to look at
        Vector3 targetLookAtPoint = objectGrabbed.GetComponent<Transform>().position - grabLookPosition.position;
        //Create a position off of the point
        targetLookAtPoint = new Vector3(targetLookAtPoint.x, transform.position.y, targetLookAtPoint.z);
        //Normalize the data (Same direction, but magnitude is 1)
        targetLookAtPoint.Normalize();
        //Do the slerp thing to interpolate the rotation towards the player
        targetLookAtPoint = Vector3.Slerp(transform.forward, targetLookAtPoint, Time.deltaTime * rotSpeed);
        //Update our target point with current position
        targetLookAtPoint += transform.position;
        //Look at player
        objectGrabbed.GetComponent<Transform>().LookAt(targetLookAtPoint);
    }
}
