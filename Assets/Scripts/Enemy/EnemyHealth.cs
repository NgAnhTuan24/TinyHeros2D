using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private float knockBackThrust = 20; //lực đẩy;

    private int hp;
    private Knockback knockback;
    private Flash flash;
    
    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        hp = maxHp;    
    }

    public void TakeDamage(int damage, Transform damageSource)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);

        Debug.Log("Quái nhận: " + damage + " sát thương, máu hiện tại: " + hp);

        knockback.GetKnockedBack(damageSource, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());

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