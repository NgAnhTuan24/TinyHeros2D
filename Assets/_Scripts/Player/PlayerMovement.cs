using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private int maxJumpCount = 2;
    [SerializeField] private float gravityScale = 5f;

    [SerializeField] private ParticleSystem dustRun;
    [SerializeField] private ParticleSystem dustJump;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCol2D;
    private Knockback knockback;

    float inputX;
    bool isGrounded;
    int jumpCount;

    public bool isLocked;

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
        DustEffect();
        Flip();
        UpdateAnim();
    }

    private void FixedUpdate()
    {
        if (knockback.gettingKnockedBack) return;

        CheckGround();
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
    }

    #region UI Button Call - Input Mobile
    public void MoveLeft()
    {
        if (isLocked) return;
        inputX = -1;
    }

    public void MoveRight()
    {
        if (isLocked) return;
        inputX = 1;
    }

    public void StopMove()
    {
        inputX = 0;
    }

    public void JumpButton()
    {
        if (isLocked) return;
        if (jumpCount < maxJumpCount) Jump();
    }
    #endregion 

    void Jump()
    {
        if (isGrounded)
        {
            dustJump.Play();
        }

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
            dustJump.Play();
        }
    }

    void DustEffect()
    {
        if (isGrounded && Mathf.Abs(inputX) > 0.1f && !dustRun.isPlaying)
        {
            dustRun.Play();
        }
        else if (!isGrounded || Mathf.Abs(inputX) <= 0.1f)
        {
            dustRun.Stop();
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

    public void LockPlayer()
    {
        isLocked = true;
        inputX = 0;

        rb.velocity = Vector2.zero;

        anim.SetFloat("Speed", 0);
    }

    public void UnlockPlayer()
    {
        isLocked = false;
    }
}
