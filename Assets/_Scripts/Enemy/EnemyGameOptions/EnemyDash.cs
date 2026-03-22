using System.Collections;
using UnityEngine;

public class EnemyDash : MonoBehaviour
{
    public float dashSpeed = 10f;
    public float dashTime = 0.5f;
    public float stopDistance = 1.5f;

    private Rigidbody2D rb;
    private Transform player;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    public IEnumerator DoDash()
    {
        animator.SetTrigger("Dash");

        Vector2 startPos = transform.position;
        Vector2 playerPos = player.position;

        float dirX = Mathf.Sign(playerPos.x - startPos.x);

        float targetX = playerPos.x - dirX * stopDistance;

        float t = 0;

        while (t < dashTime)
        {
            float newX = Mathf.Lerp(startPos.x, targetX, t / dashTime);

            rb.MovePosition(new Vector2(newX, rb.position.y));

            t += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(new Vector2(targetX, rb.position.y));
    }
}