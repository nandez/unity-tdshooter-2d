using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speed = 0.2f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 newPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
    }
}