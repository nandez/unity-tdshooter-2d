using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Camera mainCamera;
    public Rigidbody2D playerRigidBody;

    private Vector2 movement;
    private Vector2 mousePosition;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        // Handles the player movement...
        playerRigidBody.MovePosition(playerRigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Handles the player aiming...
        Vector2 lookDir = mousePosition - playerRigidBody.position;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        playerRigidBody.rotation = angle;
    }

    protected void TrackCamera()
    {
        /*var mousePosition = Input.mousePosition;
        mousePosition.z = -20;

        var position = mainCamera.WorldToScreenPoint(transform.position);
        mousePosition.x -= position.x;
        mousePosition.y -= position.y;

        var angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);*/

        Vector2 mousePosition = mainCamera.WorldToScreenPoint(Input.mousePosition);
        Vector2 lookDir = mousePosition - playerRigidBody.position;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        playerRigidBody.rotation = angle;
    }
}