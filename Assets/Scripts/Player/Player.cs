﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Setup player control variables
    public float maxVelocity;
    public float acceleration;
    public float sprintFactor;
    public float jumpForce;
    public float fallSpeed;
    public float minimumTerrainAngle;
    public float angleBoost;
    //Store the distance to ground
    [HideInInspector] float distToGround;
    //Setup the different force directions
    [HideInInspector] Vector3 forward = new Vector3();
    [HideInInspector] Vector3 left = new Vector3();
    [HideInInspector] Vector3 right = new Vector3();
    [HideInInspector] Vector3 back = new Vector3();
    //Grab our rigidbody to control forces
    [HideInInspector] Rigidbody rb;
    //Setup movement booleans
    private bool moveForward = false;
    private bool moveBack = false;
    private bool moveLeft = false;
    private bool moveRight = false;

    // Start is called before the first frame update
    void Start()
    {
        //Grab player rigid body component
        rb = GetComponent<Rigidbody>();
        //Setup forces for basic player movement
        UpdateTargetVelocity(maxVelocity);
        // get the distance to ground
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Move Forward
        if (Input.GetKey("w"))
        {
            moveForward = true;
        } else
        {
            moveForward = false;
        }

        //Move Backwards
        if (Input.GetKey("s"))
        {
            moveBack = true;
        }
        else
        {
            moveBack = false;
        }

        //Move Left
        if (Input.GetKey("a"))
        {
            moveLeft = true;
        }
        else
        {
            moveLeft = false;
        }

        //Move Right
        if (Input.GetKey("d"))
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
        //Check if shift is pressed and add sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Add to target velocity and acceleration power
            UpdateTargetVelocity(maxVelocity * sprintFactor);
            acceleration *= sprintFactor;
        }
        //When shift is released, go back to normal speed
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //Decrease target velocity and acceleration power
            UpdateTargetVelocity(maxVelocity);
            acceleration /= sprintFactor;
        }
        //Check if space is hit for jumping and check if player is on ground
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    //Fixed update is called zero, once, or multiple times per frame
    void FixedUpdate()
    {
        //Call our function to move forward
        if (moveForward)
        {
            AccelerateTo(forward, acceleration);
        }
        //Call our function to move backwards
        if (moveBack)
        {
            AccelerateTo(back, acceleration);
        }
        //Call our function to move left
        if (moveLeft)
        {
            AccelerateTo(left, acceleration);
        }
        //Call our function to move right
        if (moveRight)
        {
            AccelerateTo(right, acceleration);
        }

        //If player is in the air, increase gravity for a quicker fall time
        if (!IsGrounded())
        {
            rb.AddForce(Physics.gravity * (rb.mass * rb.mass * fallSpeed));
        }

        //Implement braking if no button is pressed
        if (!Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d") && IsGrounded())
        {
            rb.velocity *= 0.85f;
        }
    }
    /**
     * Function that applies force to move the character with top speed and acceleration
     */
    public void AccelerateTo(Vector3 targetVelocity, float maxAccel)
    {
        //Setup our temporary vector
        Vector3 tempVec = rb.velocity;
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
        {
            //Check if slope is great enough to apply boost
            if (CheckSlope() > minimumTerrainAngle || CheckSlope() < -minimumTerrainAngle)
            {
                accel = accel.normalized * maxAccel * (System.Math.Abs(CheckSlope()) * angleBoost);
            //Otherwise, normalize the player's speed
            } else
            {
                accel = accel.normalized * maxAccel;
            }
        }
        //Apply the force
        rb.AddRelativeForce(accel, ForceMode.Acceleration);
    }
    /**
     * Checks the slope below player to determine if a boost needs to be applied
     */
    private float CheckSlope()
    {
        float slope = 0f;
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, .25f, Vector3.down, out hit, 3f))
        {
            slope = Vector3.Dot(transform.right, (Vector3.Cross(Vector3.up, hit.normal)));
        }
        return slope;
    }
    /**
     * Function to update all velocities to one speed
     */
    void UpdateTargetVelocity(float target)
    {
        this.forward.Set(0, 0, target);
        this.back.Set(0, 0, -target);
        this.left.Set(-target, 0, 0);
        this.right.Set(target, 0, 0);
    }
    /**
     * Check if player is on ground
     */
    bool IsGrounded()
    {
        //Raycast to determine if player is on ground, 0.1f is the play in case the ground isn't perfectly even
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
