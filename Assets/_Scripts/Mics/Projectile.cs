using UnityEngine;

public enum Team
{
    None,
    Player,
    Enemy
}

public class Projectile : MonoBehaviour
{
    public int dmg = 1;
    public float speed = 8f;
    public float lifeTime = 4f;
    public Team ownerTeam;
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
        if (col.CompareTag(ownerTeam.ToString()))
            return;

        IDamageable damageable = col.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(dmg, transform);
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
