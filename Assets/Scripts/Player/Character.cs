using UnityEngine;

public class Character
{
    public enum Direction
    {
        Right, 
        Left
    }

    protected int hp;
    protected float speed;
    protected float jumpForce;
    protected float gravityScale;
    protected int maxJumpCount = 2;
    protected int jumpCount;
    protected Direction direction;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected BoxCollider2D boxCollider2D;
    protected Transform tf;

    private float horizontalInput;
    private bool isGrounded;

    protected Character(GameObject gameObject)
    {
        hp = 3;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        tf = gameObject.transform;
        gravityScale = 5f;
        rb.gravityScale = gravityScale;
        jumpForce = 15f;
        jumpCount = 0;
    }

    public void Update()
    {
        CheckGround();
        InputHandle();
        FlipSprite();
        UpdateAnimation();
    }

    public void FixedUpdate()
    {
        Move();
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        animator.SetBool("IsJumping", !isGrounded && rb.velocity.y > 0.1f);
        animator.SetBool("IsFalling", !isGrounded && rb.velocity.y < -0.1f);
    }

    private void InputHandle()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput < 0)
        {
            direction = Direction.Left;
        }
        else if (horizontalInput > 0)
        {
            direction = Direction.Right;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            Jump();
        }
    }

    protected void CheckGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));

        bool wasGrounded = isGrounded;
        isGrounded = hit.collider != null;

        if (!wasGrounded && isGrounded)
        {
            jumpCount = 0;
        }
    }

    private void FlipSprite()
    {
        tf.rotation = direction == Direction.Right ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
    }

    private void Move()
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        jumpCount++;

        animator.SetBool("IsJumping", true);
        animator.SetBool("IsFalling", false);
    }
}