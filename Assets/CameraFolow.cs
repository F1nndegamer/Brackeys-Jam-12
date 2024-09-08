using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The player or object the camera will follow
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optionally, keep the camera from rotating
        transform.rotation = Quaternion.identity;
    }
}
