using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Knockback knockback;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
    }

    public void Chase(Vector2 target, float speed)
    {
        if (knockback.gettingKnockedBack) return;

        Vector2 dir = (target - (Vector2)transform.position).normalized;
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

        if (dir.x != 0) transform.localScale = new Vector3(Mathf.Sign(dir.x), 1, 1);
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
