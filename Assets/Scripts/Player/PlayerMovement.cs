using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Public variables that can be set by user
    public new Rigidbody rigidbody;
    public float targetVelocity;
    public float acceleration;
    public float sprintFactor;
    public float jumpForce;
    //Setup the different force directions (They are intialized in start)
    [HideInInspector] Vector3 forward = new Vector3();
    [HideInInspector] Vector3 left = new Vector3();
    [HideInInspector] Vector3 right = new Vector3();
    [HideInInspector] Vector3 back = new Vector3();
    //Setup a distance to ground variable
    [HideInInspector] float distToGround;

    // Start is called before the first frame update
    void Start()
    {
        //Grab player rigid body component
        rigidbody = GetComponent<Rigidbody>();
        //Setup forces for basic player movement
        UpdateTargetVelocity(targetVelocity);
        // get the distance to ground
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if shift is pressed and add sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Add to target velocity and acceleration power
            UpdateTargetVelocity(targetVelocity * sprintFactor);
            acceleration *= sprintFactor;
        }
        //When shift is released, go back to normal speed
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //Decrease target velocity and acceleration power
            UpdateTargetVelocity(targetVelocity);
            acceleration /= sprintFactor;
        }
        //Check if space is hit for jumping and check if player is on ground
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rigidbody.AddForce(0, jumpForce, 0);
        }
    }

    //Fixed update is called zero, once, or multiple times per frame
    void FixedUpdate()
    {

        if (Input.GetKey("w"))
        {
            AccelerateTo(forward, acceleration);
        }

        if (Input.GetKey("s"))
        {
            AccelerateTo(back, acceleration);
        }
        if (Input.GetKey("a"))
        {
            AccelerateTo(left, acceleration);
        }

        if (Input.GetKey("d"))
        {
            AccelerateTo(right, acceleration);
        }

    }
    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 200), "rigidbody velocity: " + rigidbody.velocity);
    }
    //Function that applies force to move the character with top speed and acceleration
    public void AccelerateTo(Vector3 targetVelocity, float maxAccel)
    {
        //Setup our temporary vector
        Vector3 tempVec = rigidbody.velocity;
        //Figure out which axis (will always be x/z) we are working with
        if (targetVelocity.x != 0)
        {
            tempVec.Set(tempVec.x, 0, 0);
        }
        else if (targetVelocity.z != 0)
        {
            tempVec.Set(0, 0, tempVec.z);
        }
        //Setup acceleration
        Vector3 deltaV = targetVelocity - tempVec;
        Vector3 accel = deltaV / Time.deltaTime;
        //Check if we are at max speed, if so, cut the power to keep a consistent top speed
        if (accel.sqrMagnitude > maxAccel * maxAccel)
            accel = accel.normalized * maxAccel;
        //Apply the force
        rigidbody.AddRelativeForce(accel, ForceMode.Acceleration);
    }
    //Function to update all velocities to one speed
    void UpdateTargetVelocity(float target)
    {
        this.forward.Set(0, 0, target);
        this.back.Set(0, 0, -target);
        this.left.Set(-target, 0, 0);
        this.right.Set(target, 0, 0);
    }
    //Check if player is on ground
    bool IsGrounded() {
        //Raycast to determine if player is on ground, 0.1f is the play in case the ground isn't perfectly even
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}