using UnityEngine;

public class GateController : MonoBehaviour
{
    //Grab the game object that will be hit with an animation
    public GameObject gate;
    //Store the animation to save CPU
    [HideInInspector] private Animator animate;
    private void Start()
    {
        //Grab the animate component on our selected gameobject
        animate = gate.GetComponent<Animator>();
    }
    //Runs when a collision is detected
    void Update()
    {
        //Check to make sure the user is trying to open gate
        if (CameraUse.isUsing)
        {
            //Check that the player is what we are colliding with and not some object
            if (CameraUse.objectToUse.tag.Equals("Button"))
            {
                //Looks for animator in object's children and set trigger
                animate.SetTrigger("OpenClose");
                //Play button animation
                GetComponent<Animator>().Play("PushButton");
            }
        }
    }
}
