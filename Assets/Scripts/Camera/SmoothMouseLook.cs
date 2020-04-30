using UnityEngine;

public class SmoothMouseLook : MonoBehaviour
{
    public float sensitivity;
    // Setup y axis variables that user can manipulate
    public float minimumY = -60F;
    public float maximumY = 60F;
    // Setup variables for mouse sensitivity
    private float rotationY = 0F;
    private Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }
    void LateUpdate()
    {
        // Only allow mouse movement if game is not paused
        if (!PauseMenu.gameIsPaused)
        {
            // Move the rigidbody the camera is attached to on the X-axis
            Quaternion rotation = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensitivity, Vector3.up);
            myRigidbody.MoveRotation(myRigidbody.rotation * rotation);
            // Grab velocity from the mouse
            rotationY += Input.GetAxis("Mouse Y") * sensitivity;
            // Clamp the new angle
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            // Rotate the camera
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
            transform.localRotation = yQuaternion;
        }
    }
    // Function created by some smart dude that clamps the mouse angle using normalization
    public static float ClampAngle(float angle, float min, float max)
    {
        angle %= 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}