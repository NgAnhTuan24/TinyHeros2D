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
    protected int damage;
    protected float gravityScale;
    protected int jumpCount;
    protected Direction direction;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected BoxCollider2D boxCollider2D;
    protected Transform tf;

    private float horizontalInput;
    private bool isGrounded;
    private int maxJumpCount = 2;

    public int Damage => damage;

    protected Character(GameObject gameObject)
    {
        hp = 3;
        speed = 7f;
        damage = 1;
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
        InputHandle();
        FlipSprite();
        UpdateAnimation();
    }

    public void FixedUpdate()
    {
        CheckGround();
        Move();
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        animator.SetBool("IsJumping", !isGrounded && rb.velocity.y > 0.2f);
        animator.SetBool("IsFalling", !isGrounded && rb.velocity.y < -0.2f);
    }

    private void InputHandle()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
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
        if (direction == Direction.Right)
            tf.localScale = new Vector3(1, 1, 1);
        else
            tf.localScale = new Vector3(-1, 1, 1);
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