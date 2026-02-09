using UnityEngine;

public enum FacingDirection
{
    Right = 1,
    Left = -1
}

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Knockback knockback;

    [SerializeField] private FacingDirection defaultFacing = FacingDirection.Right;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
    }

    public void Chase(Vector2 target, float speed, EnemyType enemyType)
    {
        if (knockback.gettingKnockedBack) return;

        Vector2 dir = (target - (Vector2)transform.position).normalized;

        if (enemyType == EnemyType.Ground)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else // Flying
        {
            rb.velocity = dir * speed;
        }

        Flip(dir);
    }

    private void Flip(Vector2 dir)
    {
        if (dir.x == 0) return;

        float facingSign = (float)defaultFacing;
        float moveSign = Mathf.Sign(dir.x);

        transform.localScale = new Vector3(
            facingSign * moveSign,
            1,
            1
        );
    }

    public void Stop()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void SetGravity(float gravity)
    {
        rb.gravityScale = gravity;
    }
}
