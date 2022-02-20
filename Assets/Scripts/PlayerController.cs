using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;
    public Camera MainCamera;

    private bool _isMoving;
    private bool _isShooting;
    private bool _isReloding;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        TrackCamera();
    }

    protected void TrackCamera()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = -20;

        var position = MainCamera.WorldToScreenPoint(transform.position);
        mousePosition.x -= position.x;
        mousePosition.y -= position.y;

        var angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}