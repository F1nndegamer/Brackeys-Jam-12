using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The player or object the camera will follow
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        // Calculate the desired position with the target's position and the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Lock the Z position while keeping the X and Y positions smooth
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);

        // Optionally, keep the camera from rotating
        transform.rotation = Quaternion.identity;
    }
}
