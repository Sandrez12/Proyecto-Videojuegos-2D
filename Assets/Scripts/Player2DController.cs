using UnityEngine;

public class Player2DController : MonoBehaviour
{
    private Rigidbody2D rb;
    private GroundCheck2D groundCheck;
    private SpriteRenderer sr;
    private Animator animator;

    [Header("Movimiento")]
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float jumpForce = 12f;

    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        groundCheck = GetComponentInChildren<GroundCheck2D>();
        animator = GetComponent<Animator>();

        currentSpeed = walkSpeed;
    }

    void Update()
    {
        // --- Movimiento horizontal ---
        float move = Input.GetAxisRaw("Horizontal");

        // Shift para correr
        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = runSpeed;
        else
            currentSpeed = walkSpeed;

        rb.linearVelocity = new Vector2(move * currentSpeed, rb.linearVelocity.y);

        // Flip del sprite
        if (move > 0) sr.flipX = true;
        else if (move < 0) sr.flipX = false;

        // --- Salto ---
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump"); // parámetro tipo Trigger
        }

        // --- Actualizar animaciones ---
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("isGrounded", groundCheck.IsGrounded());
        animator.SetFloat("yVelocity", rb.linearVelocity.y); // útil para salto/caída
    }
}