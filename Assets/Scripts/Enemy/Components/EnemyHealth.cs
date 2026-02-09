using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private float knockBackThrust = 30; //lực đẩy;
    [SerializeField] private GameObject deathVFX;

    private int hp;
    private EnemyAnimator animator;
    private Flash flash;
    private Knockback knockback;

    private void Awake()
    {
        animator = GetComponent<EnemyAnimator>();
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        hp = data.maxHp;    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10, transform);
        }
    }

    public void TakeDamage(int damage, Transform damageSource)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, data.maxHp);

        Debug.Log("Quái nhận: " + damage + " sát thương, máu hiện tại: " + hp);

        animator.TriggerHurt();

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

        Instantiate(deathVFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}