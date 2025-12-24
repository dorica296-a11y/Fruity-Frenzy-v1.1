using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2 : MonoBehaviour
{
    [SerializeField] private PlayerMovementState playerMovementState;

    [Header("Movement")]
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    float horizontalMovement;
    bool isFacingRight = true;

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.44f, 0.06f);
    public LayerMask groundLayer;
    bool isGrounded;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 10f;
    public float fallSpeedMultiplier = 2f;
    bool firstFall = true;

    [Header("Wall Check")]
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.05f, 0.77f);
    public LayerMask wallLayer;

    [Header("Wall Movement")]
    public float wallSlideSpeed = 2;
    bool isWallSliding;

    [Header("Player Win Condition")]
    private int coinCounter = 0;
    public TMP_Text counterText;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] public int winTarget;
    private bool isGameOver = false;

    [Header("Wall Jumping Condition")]
    bool isWallJumping;
    float wallJumpDirection;
    float wallJumpTime = 0.5f;
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f, 10f);

    // Start is called before the first frame update
    void Start()
    {
        winCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        GroundCheck();
        ProcessGravity();
        ProcessWallSlide();
        ProcessWallJump();
        ProcessFallState();

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
            Flip();
        }
    }

    private void ProcessFallState()
    {
        // FALL
        if (!isGrounded && rb.velocity.y < -0.1f && !isWallSliding && !isWallJumping && !WallCheck())
        {
            playerMovementState.SetMoveState(PlayerMovementState.MoveState.Fall);

            if (firstFall)
            {
                firstFall = false;
                jumpsRemaining = 1;
            }
            return;
        }

        // RUN
        if (isGrounded && Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            playerMovementState.SetMoveState(PlayerMovementState.MoveState.Running);
            return;
        }

        // IDLE
        if (isGrounded)
        {
            playerMovementState.SetMoveState(PlayerMovementState.MoveState.Idle);
        }
    }


    private void ProcessGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void ProcessWallSlide()
    {
        if (!isGrounded && WallCheck() && horizontalMovement != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void ProcessWallJump()
    {
        if (isWallSliding)
        {
            playerMovementState.SetMoveState(PlayerMovementState.MoveState.Wall_Jump);
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        }
        else if (wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.jumpSound);

                // Full Jump (Hold)
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);

                if (jumpsRemaining == 2)
                {
                    playerMovementState.SetMoveState(PlayerMovementState.MoveState.Jump);
                    firstFall = false;
                }
                else if (jumpsRemaining == 1 && !isWallJumping)
                {
                    playerMovementState.SetMoveState(PlayerMovementState.MoveState.Double_Jump);
                }
                jumpsRemaining--;
            }

            else if (context.canceled)
            {
                // Half Jump (Light Tap)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

                if (jumpsRemaining == 2)
                {
                    playerMovementState.SetMoveState(PlayerMovementState.MoveState.Jump);
                    firstFall = false;
                }
                else if (jumpsRemaining == 1 && !isWallJumping)
                {
                    playerMovementState.SetMoveState(PlayerMovementState.MoveState.Double_Jump);
                }
                jumpsRemaining--;
            }
        }

        // Wall Jump
        if (context.performed && wallJumpTimer > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y); //Jump away from wall
            wallJumpTimer = 0;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.jumpSound);
            playerMovementState.SetMoveState(PlayerMovementState.MoveState.Jump);

            // Force Flip
            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }

            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f); //Wall Jump = 0.5f -- Jump again 0.6f
        }
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            firstFall = true;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private bool WallCheck()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.fruitCollectedSound);
            coinCounter++;

            counterText.text = "Fruits Collected: " + coinCounter + " / " + winTarget;

            if (coinCounter >= winTarget)
            {
                WinGame();
            }
        }
    }

    private void WinGame()
    {
        isGameOver = true;
        winCanvas.SetActive(true);
        Time.timeScale = 0; // pause game
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
    }
}
