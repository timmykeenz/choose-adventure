﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInRange : MonoBehaviour
{
    public GameObject player;
    public float showDistance;
    [HideInInspector] float distance;
    [HideInInspector] public bool displayText;
    // Start is called before the first frame update
    void Start()
    {
        displayText = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate distance from object to player
        distance = Vector3.Distance(player.GetComponent<Transform>().transform.position, transform.position);
        //Make object visible when in range
        if (distance < showDistance && displayText)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
        //Otherwise, hide object
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
