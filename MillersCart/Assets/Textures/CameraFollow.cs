using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform kart; // Reference to the kart transform
    public Vector3 offset = new Vector3(0, 5, -10); // Offset position relative to the kart
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement

    void LateUpdate()
    {
        // Desired position of the camera
        Vector3 desiredPosition = kart.position + kart.TransformDirection(offset);

        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Make the camera look at the kart
        transform.LookAt(kart);
    }
}
