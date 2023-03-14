using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed, crounchSpeed, climbSpeed, jumpForce;
    private float normalSpeed, gravityScaleAtStart;

    private bool isGrounded, isCrouching, crouch;
    private bool canDoubleJump, canLadderJump;

    public Transform groundCheckPoint;
    public Transform ceilingCheckPoint;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public GameObject deathEffect;
    public GameObject collectible;
    [Range(0, 100)] public float changeToDrop;

    public LayerMask GroundLayer, EnemyLayer;
    public BoxCollider2D ceilingCollider;
    public CapsuleCollider2D bodyCollider;
    public CircleCollider2D ladderCollider;

    public Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float knockBackLength, knockBackForce, knockBackDistance, bounceForce;
    private float knockBackCounter;

    public bool stopInput, isFox, isBandit;

    private bool isOnLadder, airJump, playerHasVerticalSpeed;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        normalSpeed = moveSpeed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    void Update()
    {
        if (!PauseMenu.instance.isPaused && !stopInput)
        {
            if (knockBackCounter <= 0)
            {
                PlayerMovement();
            }
            else
            {
                knockBackCounter -= Time.deltaTime;
            }
        }

        if (stopInput == true)
        {
            isGrounded = true;
            animator.SetBool("isGrounded", isGrounded);
        }

    }

    private void PlayerMovement()
    {
        rb.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
        animator.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));

        PlayerJump();
        FlipSprite();
        if (isFox == true)
        {
            PlayerCrouch();
            ClimbLadder();
        }
        // if (isBandit == true)
        // {
        //     PlayerAttack();
        // }
    }

    // void PlayerAttack()
    // {
    //     if (Input.GetKeyDown(KeyCode.RightControl))
    //     {
    //         animator.SetTrigger("Attack");

    //         Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayer);

    //         foreach (Collider2D enemy in hitEnemies)
    //         {
    //             enemy.transform.parent.gameObject.SetActive(false);
    //         }
    //     }
    // }

    // private void OnDrawGizmosSelected()
    // {
    //     if (attackPoint == null) { return; }

    //     Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    // }

    void ClimbLadder()
    {
        if (!ladderCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = gravityScaleAtStart;
            animator.SetBool("isClimbing", false);
            animator.SetBool("isHolding", false);
            return;
        }

        canDoubleJump = true;
        canLadderJump = true;

        Vector2 climbVelocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed);
        rb.velocity = climbVelocity;
        rb.gravityScale = 0f;

        playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        animator.SetBool("isHolding", !playerHasVerticalSpeed);
        animator.SetBool("isClimbing", playerHasVerticalSpeed);
    }


    private void PlayerCrouch()
    {
        isCrouching = Physics2D.OverlapCircle(ceilingCheckPoint.position, 0.2f, GroundLayer);

        if (Input.GetButton("Crouch"))
        {
            if (ceilingCollider != null)
            {
                crouch = true;
                ceilingCollider.enabled = false;
                moveSpeed = crounchSpeed;
            }
        }
        else
        {
            if (ceilingCollider != null && !isCrouching)
            {
                crouch = false;
                moveSpeed = normalSpeed;
                ceilingCollider.enabled = true;
            }
        }
        animator.SetBool("isCrouching", crouch);
    }

    private void PlayerJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, GroundLayer);

        if (isGrounded) { canDoubleJump = true; }
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                AudioManager.instance.PlaySFX(10);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (canDoubleJump)
            {
                DoubleJump();
            }
            else if (canLadderJump)
            {
                LadderJump();
            }
            animator.SetBool("isGrounded", !isGrounded);
        }
        animator.SetBool("isGrounded", isGrounded);
    }

    private void FlipSprite()
    {
        if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;

        if (spriteRenderer.flipX == false)
        {
            rb.velocity = new Vector2(-knockBackForce * knockBackDistance, knockBackForce);
        }
        else
        {
            rb.velocity = new Vector2(knockBackForce * knockBackDistance, knockBackForce);
        }

        animator.SetTrigger("isHurt");
    }

    public void DoubleJump()
    {
        if (canDoubleJump)
        {
            AudioManager.instance.PlaySFX(10);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = false;
        }
    }

    public void LadderJump()
    {
        if (canLadderJump)
        {
            AudioManager.instance.PlaySFX(10);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canLadderJump = false;
        }
    }

    public void Bounce()
    {
        AudioManager.instance.PlaySFX(10);
        if (canDoubleJump == false)
        {
            canDoubleJump = true;
        }
        if (canLadderJump == false)
        {
            canLadderJump = true;
        }
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = other.transform;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }
}
