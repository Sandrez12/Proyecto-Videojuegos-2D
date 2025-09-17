using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private GroundCheck2D groundCheck;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<GroundCheck2D>();
    }

    void Update()
    {
        // --- Movimiento horizontal (idle / walk / run) ---
        float speed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", speed);

        // --- Saltar ---
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.IsGrounded())
        {
            animator.SetBool("isJumping", true);
        }

        // Si está en el aire y la velocidad Y es negativa => cayendo
        if (rb.linearVelocity.y < -0.1f && !groundCheck.IsGrounded())
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        // Si toca el suelo después de caer => aterrizaje
        if (groundCheck.IsGrounded() && animator.GetBool("isFalling"))
        {
            animator.SetBool("isLanding", true);
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isLanding", false);
        }

        if (groundCheck.IsGrounded() && rb.linearVelocity.y == 0)
        {
            animator.SetBool("isJumping", false);
        }
    }
}