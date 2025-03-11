using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public List<PlayerInputRecorder.InputFrame> playbackInputs;
    private int currentInputIndex = 0;
    private float playbackStartTime;

    private Rigidbody2D rb;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public float speed = 5f;
    public float jumpForce = 5f;
    public float checkDistance = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playbackStartTime = Time.time;
    }

    void FixedUpdate()
    {
        if (playbackInputs == null || playbackInputs.Count == 0)
            return;

        // Determine how much time has elapsed since playback started.
        float elapsedTime = Time.time - playbackStartTime;

        // Advance to the correct frame based on the elapsed time.
        while (currentInputIndex < playbackInputs.Count - 1 &&
               playbackInputs[currentInputIndex + 1].relativeTime <= elapsedTime)
        {
            currentInputIndex++;
        }

        // Get the current input frame.
        PlayerInputRecorder.InputFrame frame = playbackInputs[currentInputIndex];

        // Update horizontal movement.
        Vector2 vel = rb.linearVelocity;
        vel.x = frame.horizontal * speed;
        rb.linearVelocity = vel;

        // Update animation parameters for moving and sprite direction.
        if (Mathf.Abs(frame.horizontal) > 0.01f)
        {
            animator.SetBool("moving", true);
            spriteRenderer.flipX = frame.horizontal < 0;
        }
        else
        {
            animator.SetBool("moving", false);
        }

        // Check if the ghost is grounded.
        bool grounded = IsGrounded();
        animator.SetBool("airborne", !grounded);
        animator.SetFloat("airvelo", rb.linearVelocity.y);

        // If the recorded input indicates a jump and we're grounded, simulate a jump.
        if (frame.jump && grounded)
        {
            vel = rb.linearVelocity;
            vel.y = jumpForce;
            rb.linearVelocity = vel;
        }
    }

    // A simple grounded check using raycasting that ignores colliders tagged "Player" and "Ghost".
    bool IsGrounded()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, checkDistance);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null &&
                !hit.collider.CompareTag("Player") &&
                !hit.collider.CompareTag("Ghost"))
            {
                return true;
            }
        }
        return false;
    }
}
