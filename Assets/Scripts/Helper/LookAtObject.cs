using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    //Object that the GameObject will look at
    public GameObject objectToLookAt;
    //Target grabs the transform from the object
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
