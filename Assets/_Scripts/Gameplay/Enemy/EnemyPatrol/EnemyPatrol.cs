using System;
using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour, IEnemy
{
    public event Action OnDie;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 5f;

    [SerializeField] private float blinkDuration = 1f;
    [SerializeField] private float blinkInterval = 0.1f;

    [SerializeField] private bool spriteFacingRight = true;

    private Vector3 startPos;
    private bool movingLeft = true;
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyDrop drop;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        drop = GetComponent<EnemyDrop>();
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;

        // Đổi hướng trước (tránh bị vượt biên)
        if (rb.position.x <= leftBound)
        {
            movingLeft = false;
        }
        else if (rb.position.x >= rightBound)
        {
            movingLeft = true;
        }

        float dir = movingLeft ? -1f : 1f;
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);

        // Flip ổn định
        Vector3 scale = transform.localScale;
        scale.x = spriteFacingRight
            ? Mathf.Sign(dir) * Mathf.Abs(scale.x)
            : -Mathf.Sign(dir) * Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    public void Die()
    {
        OnDie?.Invoke();

        if (!rb.simulated) return;

        rb.velocity = Vector2.zero;
        rb.simulated = false;

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }

        anim.SetBool("IsDie", true);

        StartCoroutine(DieRoutine());

        drop.Drop();
    }

    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(
            anim.GetCurrentAnimatorStateInfo(0).length
        );

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float timer = 0f;

        while (timer < blinkDuration)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        Destroy(gameObject);
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