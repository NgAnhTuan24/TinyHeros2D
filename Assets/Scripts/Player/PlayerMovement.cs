using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private int maxJumpCount = 2;
    [SerializeField] private float gravityScale = 5f;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCol2D;
    private Knockback knockback;

    float inputX;
    bool isGrounded;
    int jumpCount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCol2D = GetComponent<BoxCollider2D>();
        knockback = GetComponent<Knockback>();

        rb.gravityScale = gravityScale;
    }

    private void Update()
    {
        inputX = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount) Jump();

        Flip();
        UpdateAnim();
    }

    private void FixedUpdate()
    {
        if (knockback.gettingKnockedBack) return;

        CheckGround();
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        jumpCount++;

        anim.SetBool("IsJumping", true);
        anim.SetBool("IsFalling", false);
    }

    void CheckGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));

        bool wasGround = isGrounded;
        isGrounded = hit.collider != null;

        if (!wasGround && isGrounded)
        {
            jumpCount = 0;
        }
    }

    void Flip()
    {
        if (inputX == 0) return;
        transform.localScale = new Vector3(Mathf.Sign(inputX), 1, 1);
    }

    void UpdateAnim()
    {
        anim.SetFloat("Speed", Mathf.Abs(inputX));
        anim.SetBool("IsJumping", !isGrounded && rb.velocity.y > 0.2f);
        anim.SetBool("IsFalling", !isGrounded && rb.velocity.y < -0.2f);
    }
}
