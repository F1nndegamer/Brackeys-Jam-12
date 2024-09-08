using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 movement;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input for movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Rotate the player based on movement direction
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void FixedUpdate()
    {
        // Apply movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
