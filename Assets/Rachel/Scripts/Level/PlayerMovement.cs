using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Attributes")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Animation")]
    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isJumping = false;
    private bool canMove = true;
    private Vector3 ogScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ogScale = animator.transform.localScale;
    }

    private void Update()
    {
        if (canMove) CheckInput();
    }

    private void FixedUpdate()
    {
        if (canMove) Move();
    }

    #region Movement Logic
    private void CheckInput()
    {
        if (Gamepad.current != null && Gamepad.current.leftStick.ReadValue() != Vector2.zero)
        {
            moveInput = Gamepad.current.leftStick.ReadValue();
            Debug.Log(moveInput);

            // animate
            animator.SetBool("isWalking", true);
            animator.transform.localScale = new Vector3(ogScale.x * (moveInput.x >= 0 ? 1 : -1), ogScale.y, ogScale.z);
        }
        else
        {
            moveInput = Vector2.zero;
            animator.SetBool("isWalking", false);
        }

        isJumping = (Gamepad.current != null && Gamepad.current.buttonSouth.isPressed);
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (isJumping && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private bool IsGrounded()
    {
        int playerLayer = 9;
        int layerMask = ~(1 << playerLayer);

        return Physics2D.Raycast(transform.position, Vector2.down, 1.1f, layerMask);
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;
    }
    #endregion
}
