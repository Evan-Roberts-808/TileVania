using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rigidbody;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    float startingGravity;

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        startingGravity = rigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            rigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) {
            rigidbody.gravityScale = startingGravity;
            animator.SetBool("isClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(rigidbody.velocity.x, moveInput.y * climbSpeed);
        rigidbody.velocity = climbVelocity;
        bool playerHasVerticalSpeed = Mathf.Abs(rigidbody.velocity.y) > Mathf.Epsilon;
        rigidbody.gravityScale = 0f;
        animator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidbody.velocity.y);
        rigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody.velocity.x), 1f);
        }
    }
}
