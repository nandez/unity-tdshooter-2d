using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICanTakeDamage
{
    public float moveSpeed = 5f;
    public Camera mainCamera;
    public Rigidbody2D playerRigidBody;
    public Animator playerAnimator;
    public int Health = 10;


    private Vector2 movement;
    private Vector2 mousePosition;
    private int currentHealth;

    private void Start()
    {
        currentHealth = Health;
    }

    private void Update()
    {
        // Captures the player input..
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Updates the animator variables for movement..
        playerAnimator.SetFloat("Horizontal", movement.x);
        playerAnimator.SetFloat("Vertical", movement.y);

        // Captures the player's mouse current position..
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Debug.Log("Player dies...");
        }
    }
}