using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [HideInInspector] new Rigidbody rigidbody;
    public float sensitivityX;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Look();   
    }

    public void Look()
    {
        Quaternion rotation = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensitivityX, Vector3.up);
        rigidbody.MoveRotation(rigidbody.rotation * rotation);
    }
}