using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int dmg = 1;
    public float lifeTime = 4f;

    void Start()
    {
        Destroy(gameObject, lifeTime);    
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        EnemyHealth enemy = col.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(dmg);
            Destroy(gameObject);
        }
    }
}
