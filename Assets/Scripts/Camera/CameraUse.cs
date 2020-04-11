using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUse : MonoBehaviour
{
    //Setup user manipulatable grab parameters
    public string useKey;
    public int useDistance;
    [HideInInspector] public RaycastHit hit;
    //Variables to check which object we are using and whether we are hitting them.
    [HideInInspector] public static bool isUsing;
    [HideInInspector] public static string usedKey;
    [HideInInspector] public static GameObject objectToUse;

    // Start is called before the first frame update
    void Start()
    {
        //Set variable defaults
        objectToUse = null;
        isUsing = false;
        usedKey = useKey;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the grab key is pressed, they are looking at an object, and that the object is grabbable
        if (Input.GetKeyDown(useKey) && Physics.Raycast(transform.position, transform.forward, out hit, useDistance) && hit.transform.GetComponent<Rigidbody>())
        {
            //Set isUsing to true as we are currently attempt use an item (useKey is held down and we are in distance)
            isUsing = true;
            //If so, try using the object
            objectToUse = hit.transform.gameObject;
        } else
        {
            //If we not using an item, this should be false
            isUsing = false;
        }
    }
}
