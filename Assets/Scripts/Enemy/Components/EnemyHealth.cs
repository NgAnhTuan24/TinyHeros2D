using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private float knockBackThrust = 30; //lực đẩy;

    private int hp;
    private EnemyController controller;
    private EnemyAnimator animator;
    private Flash flash;
    private Knockback knockback;

    private void Awake()
    {
        controller = GetComponent<EnemyController>();
        animator = GetComponent<EnemyAnimator>();
        flash = GetComponent<Flash>();
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

        animator.TriggerHurt();
        controller.ChangeState(EnemyState.Hurt);

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