using UnityEngine;

public class PlaceBattery : MonoBehaviour
{
    public static bool isConnected;

    private void Start()
    {
        //Make sure the battery resets to disconnected on level start
        isConnected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks if the tag is a battery
        if (other.tag.Equals("Battery") && !isConnected)
        {
            //Set isConnected
            isConnected = true;
            //Make it so the Rigidbody can't move
            other.GetComponent<Rigidbody>().isKinematic = true;
            //Update the object's parent
            other.GetComponent<Transform>().parent = this.GetComponent<Transform>();
            //Update the object's position relative to the parent
            other.GetComponent<Transform>().localPosition = new Vector3(0, -.293f, 0);
            //Update the object's rotation
            other.GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
