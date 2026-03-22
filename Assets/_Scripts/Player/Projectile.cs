using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int dmg = 1;
    public float speed = 8f;
    public float lifeTime = 4f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);    
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        EnemyHealth enemy = col.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(dmg, transform);
            Destroy(gameObject);
        }
    }

    public void Init(float dir)
    {
        if (rb != null)
        {
            rb.velocity = Vector2.right * dir * speed;
        }
    }
}
