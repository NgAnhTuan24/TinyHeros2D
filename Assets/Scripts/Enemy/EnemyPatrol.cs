using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 5f;

    private Vector3 startPos;
    private bool movingLeft = true;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;

        float dir = movingLeft ? -1f : 1f;
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);

        if (movingLeft && rb.position.x <= leftBound)
        {
            movingLeft = false;
            Flip();
        }
        else if (!movingLeft && rb.position.x >= rightBound)
        {
            movingLeft = true;
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 start = Application.isPlaying ? startPos : transform.position;

        Vector3 left = new Vector3(start.x - distance, start.y, start.z);
        Vector3 right = new Vector3(start.x + distance, start.y, start.z);

        Gizmos.DrawSphere(left, 0.1f);
        Gizmos.DrawSphere(right, 0.1f);
        Gizmos.DrawLine(left, right);
    }

}
