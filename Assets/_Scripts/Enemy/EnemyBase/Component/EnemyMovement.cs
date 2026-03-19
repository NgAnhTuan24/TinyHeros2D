using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float patrolSwitchTime = 2f;
    private float patrolTimer;

    private Rigidbody2D rb;
    private EnemyController enemy;

    private Vector2 patrolDirection = Vector2.right;
    private bool facingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyController>();
    }

    public void Patrol()
    {
        if (patrolTimer <= 0)
        {
            FlipDirection();
            patrolTimer = patrolSwitchTime;
        }

        patrolTimer -= Time.deltaTime;

        float dir = patrolDirection.x;

        HandleFlip(dir);

        rb.velocity = new Vector2(dir * enemy.MoveSpeed, rb.velocity.y);
    }

    public void ChasePlayer()
    {
        Transform player = enemy.Detection.GetPlayer();
        if (player == null) return;

        float dir = Mathf.Sign(player.position.x - transform.position.x);

        HandleFlip(dir);

        rb.velocity = new Vector2(dir * enemy.ChaseSpeed, rb.velocity.y);
    }

    public void Stop()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void ResetPatrolTimer()
    {
        patrolTimer = patrolSwitchTime;
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void HandleFlip(float direction)
    {
        if (direction > 0 && !facingRight)
        {
            Flip();
        }

        else if (direction < 0 && facingRight)
        {
            Flip();
        }
    }

    private void FlipDirection()
    {
        patrolDirection *= -1;
    }
}
