using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float jumpForce = 5f;
    public float checkDistance = 1.0f;
    private bool jumpPressed = false;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerInputRecorder inputRecorder;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputRecorder = GetComponent<PlayerInputRecorder>();
    }

    void Update()
    {
        // Capture jump input in Update
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && GameManager.Instance != null && GameManager.Instance.playerControlsEnabled)
        {
            jumpPressed = true;
        }

        playerAnimator.SetFloat("airvelo", rb.linearVelocityY);
        IsGrounded();

        if (Input.GetKeyDown(KeyCode.R) && GameManager.Instance != null && GameManager.Instance.playerControlsEnabled)
        {
            if (GameManager.Instance != null && inputRecorder != null)
            {
                // Save the current run's recorded inputs as a ghost run.
                GameManager.Instance.AddRun(inputRecorder.recordedInputs);
            }
            // Reset the level (ghosts will be spawned as defined in GameManager).
            GameManager.Instance.ResetLevel();
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.playerControlsEnabled)
        {
            float move = Input.GetAxis("Horizontal");
            if (move != 0)
            {
                playerAnimator.SetBool("moving", true);
                spriteRenderer.flipX = move < 0;
            }
            else playerAnimator.SetBool("moving", false);

            Vector2 vel = rb.linearVelocity;
            vel.x = move * speed;
            rb.linearVelocity = vel;

            if (jumpPressed)
            {
                vel = rb.linearVelocity;
                vel.y = jumpForce;
                rb.linearVelocity = vel;
                jumpPressed = false;
            }
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, checkDistance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null &&
                !hit.collider.CompareTag("Player") &&
                !hit.collider.CompareTag("Ghost"))
            {
                playerAnimator.SetBool("airborne", false);
                return true;
            }
        }

        playerAnimator.SetBool("airborne", true);
        return false;
    }
}
