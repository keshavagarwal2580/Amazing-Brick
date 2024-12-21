using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f;  // Vertical force
    public float sideForce = 1f; // Horizontal force
    private Rigidbody2D rb;

    [SerializeField] private GameObject startElement;

    private bool isGameStart = false; // To track if the game has started

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Initially disable gravity to prevent falling
    }

    void Update()
    {
        // Wait for first tap to enable gravity and allow movement
        if (!isGameStart && Input.GetMouseButtonDown(0)) // For mobile, use touch input
        {
            startElement.SetActive(false);
            isGameStart = true;
            rb.gravityScale = 1; // Enable gravity after the first tap
        }

        if (isGameStart) // Allow movement only after the game has started
        {
            HandlePlayerMovement();
        }
    }

    void HandlePlayerMovement()
    {
        if (Input.GetMouseButtonDown(0)) // For mobile, use touch input
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (touchPosition.x < 0) // Left screen
            {
                Jump(-sideForce); // Move left
            }
            else if (touchPosition.x > 0) // Right screen
            {
                Jump(sideForce); // Move right
            }
        }
    }

    void Jump(float horizontalForce)
    {
        rb.velocity = Vector2.zero; // Reset velocity for consistent jumping
        rb.AddForce(new Vector2(horizontalForce, jumpForce), ForceMode2D.Impulse); // Apply the force to jump
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("End Game") || other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("Main Menu");
            Debug.Log("Game Over");
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("Main Menu");
            Debug.Log("Game Over");
        }
    }
}
