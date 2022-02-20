using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Camera mainCamera;
    public Rigidbody2D playerRigidBody;

    private Vector2 movement;

    private void Awake()
    {
        
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");



        TrackCamera();
    }

    private void FixedUpdate()
    {
        playerRigidBody.MovePosition(playerRigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    protected void TrackCamera()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = -20;

        var position = mainCamera.WorldToScreenPoint(transform.position);
        mousePosition.x -= position.x;
        mousePosition.y -= position.y;

        var angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


}