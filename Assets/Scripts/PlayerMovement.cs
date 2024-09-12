using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private FishingMachanic fishingMachanic;
    [SerializeField] private Rigidbody2D rb;
    
    [Header("References")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 0.2f;


    [Header("Sprites")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] directionSprites;

    private Vector2 movement;
    private int lastDirectionIndex;
    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (fishingMachanic == null) fishingMachanic = GetComponent<FishingMachanic>();
    }

    void Update()
    {
        if (fishingMachanic.IsFishing) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        UpdateSpriteDirection(movement);
        // Rotate the player based on movement direction
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotateSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (fishingMachanic.IsFishing) return;
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement.normalized);
    }

    // This function maps movement direction to one of 9 sprite orientations
    private void UpdateSpriteDirection(Vector2 movement)
    {
        if (movement == Vector2.zero)
        {
            // If idle, keep showing the last direction sprite
            spriteRenderer.sprite = directionSprites[lastDirectionIndex];
            return;
        }

        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        int directionIndex = GetDirectionIndex(angle);

        // Update the sprite and store the last direction
        spriteRenderer.sprite = directionSprites[directionIndex];
        lastDirectionIndex = directionIndex;
    }

    private int GetDirectionIndex(float angle)
    {
        if (angle >= -22.5f && angle < 22.5f)
        {
            // Right
            return 2;
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            // Up-right
            return 3;
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            // Up
            return 4;
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            // Up-left
            return 5;
        }
        else if (angle >= 157.5f || angle < -157.5f)
        {
            // Left
            return 6;
        }
        else if (angle >= -157.5f && angle < -112.5f)
        {
            // Down-left
            return 7;
        }
        else if (angle >= -112.5f && angle < -67.5f)
        {
            // Down
            return 0;
        }
        else if (angle >= -67.5f && angle < -22.5f)
        {
            // Down-right
            return 1;
        }

        // Default to idle if angle doesn't match any range (shouldn't happen)
        return 8;
    }
    public void IncreaseSpeed(float amount)
    {
        moveSpeed += amount;
    }
}
