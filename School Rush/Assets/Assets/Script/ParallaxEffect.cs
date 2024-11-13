using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxIntensity = 0.02f;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += deltaMovement * parallaxIntensity;
        lastCameraPosition = cameraTransform.position;
    }
}
