using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private FishingMachanic fishingMachanic;
    [SerializeField] private Rigidbody2D rb;
    
    [Header("References")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 0.2f;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] directionSprites;

    private Vector2 movement;
    private Animator animator;
    private string currentAnimationName;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (fishingMachanic == null) fishingMachanic = GetComponent<FishingMachanic>();
    }

    void Update()
    {
        if (fishingMachanic.IsFishing) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Rotate the player based on movement direction
        if (movement != Vector2.zero)
        {
            ChangeAnimation("Sail");
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotateSpeed * Time.deltaTime);
        }
        else
        {
            ChangeAnimation("Idle");
        }
    }

    void FixedUpdate()
    {
        if (fishingMachanic.IsFishing) return;
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement.normalized);
    }
    public void IncreaseSpeed(float amount)
    {
        moveSpeed += amount;
    }
    private void ChangeAnimation(string animationName)
    {
        if (currentAnimationName != animationName)
        {
            animator.ResetTrigger(animationName);
            currentAnimationName = animationName;
            animator.SetTrigger(currentAnimationName);
        }
    }
}
