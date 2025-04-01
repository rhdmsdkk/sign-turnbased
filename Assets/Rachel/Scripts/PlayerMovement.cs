using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class PlayerMovement : MonoBehaviour
{
    [Header("Attributes")]
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isJumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Gamepad.current != null && Gamepad.current.leftStick.ReadValue() != Vector2.zero)
        {
            moveInput = Gamepad.current.leftStick.ReadValue();
        }
        else
        {
            moveInput = Vector2.zero;
        }

        isJumping = (Gamepad.current != null && Gamepad.current.buttonSouth.isPressed);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        if (isJumping && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private bool IsGrounded()
    {
        int playerLayer = 9;
        int layerMask = ~(1 << playerLayer);

        return Physics2D.Raycast(transform.position, Vector2.down, 1.1f, layerMask);
    }
}
