using UnityEngine;

public abstract class Character 
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

    internal void Update()
    {
        InputHandle();
        FlipSprite();
    }

    protected abstract void InputHandle();

    private void FlipSprite()
    {
        switch (direction)
        {
            case Direction.Left:
                tf.localScale = new Vector3(-1, tf.localScale.y, tf.localScale.z);
                break;
            case Direction.Right:
                tf.localScale = new Vector3(1, tf.localScale.y, tf.localScale.z);
                break;
        }
    }
}
