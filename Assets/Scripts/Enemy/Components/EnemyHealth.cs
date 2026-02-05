using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private float knockBackThrust = 20; //lực đẩy;

    private int hp;
    private Knockback knockback;
    
    private void Awake()
    {
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        hp = data.maxHp;    
    }

    public void TakeDamage(int damage, Transform damageSource)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, data.maxHp);

        Debug.Log("Quái nhận: " + damage + " sát thương, máu hiện tại: " + hp);

        knockback.GetKnockedBack(damageSource, knockBackThrust);

        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Quái đã chết");
        Destroy(gameObject);
    }
}