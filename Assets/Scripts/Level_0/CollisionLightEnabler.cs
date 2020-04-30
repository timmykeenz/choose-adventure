using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLightEnabler : MonoBehaviour
{
    public GameObject objectToCollide;
    public GameObject lightToEnable;
    bool hasTriggered;

    void Start()
    {
        hasTriggered = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!hasTriggered && collider.gameObject.tag.Equals(objectToCollide.tag))
        {
            hasTriggered = true;
            lightToEnable.GetComponent<Light>().enabled = true;
        }
    }
}
