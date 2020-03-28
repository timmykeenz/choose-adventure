using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public GameObject objectToLookAt;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = objectToLookAt.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Look at the object chosen
        transform.rotation = Quaternion.LookRotation(transform.position - target.position);
    }
}
