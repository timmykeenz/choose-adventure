using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPilot : MonoBehaviour
{
    public GameObject gate;
    public static bool entryGateOpened = false;

    void Update()
    {
        if (PlaceBattery.isConnected && !entryGateOpened)
        {
            entryGateOpened = true;
            gate.GetComponentInChildren<Animator>().Play("OpenGate");
        }
    }
}
