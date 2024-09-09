using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private FishingMachanic fishingMachanic;
    [SerializeField] private Rigidbody2D rb;
    
    [Header("References")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 0.2f;

    private Vector2 movement;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (fishingMachanic == null) fishingMachanic = GetComponent<FishingMachanic>();
    }

    void Update()
    {
        // Get input for movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Rotate the player based on movement direction
        if (movement != Vector2.zero && !fishingMachanic.isFishing)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotateSpeed);
        }
    }

    void FixedUpdate()
    {
        // Apply movement
        if (!fishingMachanic.isFishing)
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement.normalized);
    }
}
