using UnityEngine;

public class Character
{
    public enum Direction
    {
        Right, 
        Left
    }

    protected int hp { get; set; }

    protected float speed { get; set; }

    protected float jumpForce { get; set; }

    protected float gravityScale { get; set; }

    protected Direction direction { get; set; }

    protected Rigidbody2D rb { get; set; }

    protected Animator animator { get; set; }

    protected BoxCollider2D boxCollider2D { get; set; }

    protected Transform tf { get; set; }

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

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    protected void CheckGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));

        isGrounded = hit.collider != null;
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
    }
}
