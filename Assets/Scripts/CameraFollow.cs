using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // The player or object the camera will follow
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float limitPosX;
    [SerializeField] private float limitPosY;
    [SerializeField] private float targetZoomSize;
    [SerializeField] private float zoomSpeed;
    private Camera thisCamera;
    private float originalCameraSize;
    private void Awake()
    {
        thisCamera = GetComponent<Camera>();
        originalCameraSize = thisCamera.orthographicSize;
    }
    void FixedUpdate()
    {
        // Calculate the desired position with the target's position and the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);

        if (transform.position.x < limitPosX)
        {
            transform.position = new Vector3(limitPosX, transform.position.y, -10);
        }
        if (Mathf.Abs(transform.position.y) > limitPosY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y > 0 ? limitPosY : - limitPosY, -10);
        }

        transform.rotation = Quaternion.identity;
    }
    public void ZoomOut()
    {
        StartCoroutine(nameof(ZoomOutCoroutine));    
    }
    public void ZoomIn()
    {
        StartCoroutine(nameof(ZoomInCoroutine));
    }
    private IEnumerator ZoomOutCoroutine()
    {
        while ((targetZoomSize - thisCamera.orthographicSize) > 0.1f)
        {
            thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, targetZoomSize, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        thisCamera.orthographicSize = targetZoomSize;
    }
    private IEnumerator ZoomInCoroutine()
    {
        while ((thisCamera.orthographicSize - originalCameraSize) > 0.1f)
        {
            thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, originalCameraSize, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        thisCamera.orthographicSize = originalCameraSize;
    }
}
