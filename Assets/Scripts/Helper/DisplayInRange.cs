using UnityEngine;

public class DisplayInRange : MonoBehaviour
{
    public GameObject player;
    public float showDistance;
    //Distance from player and boolean to determine if object should display
    private float distance;
    [HideInInspector] public bool displayObject;
    // Start is called before the first frame update
    void Start()
    {
        displayObject = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate distance from object to player
        distance = Vector3.Distance(player.GetComponent<Transform>().transform.position, transform.position);
        //Make object visible when in range
        if (distance < showDistance && displayObject)
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
