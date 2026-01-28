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

    protected float gravityScale { get; set; }

    protected Direction direction { get; set; }

    protected Rigidbody2D rb { get; set; }

    protected Animator animator { get; set; }

    protected BoxCollider2D boxCollider2D { get; set; }

    protected Transform tf { get; set; }

    protected float horizontalInput;

    protected Character(GameObject gameObject)
    {
        hp = 3;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        tf = gameObject.transform;
        gravityScale = 5;
        rb.gravityScale = gravityScale;
    }

    public void Update()
    {
        InputHandle();
        FlipSprite();
    }

    public void FixedUpdate()
    {
        Move();
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
    }

    private void FlipSprite()
    {
        tf.rotation = direction == Direction.Right ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
    }

    private void Move()
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }
}
