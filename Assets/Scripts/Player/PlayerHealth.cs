using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp;
    private int hp;

    Knockback knockback;
    Flash flash;

    void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    void Start()
    {
        hp = maxHp;
    }

    public void TakeDamage(int damage , Transform damageSource)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);

        Debug.Log("Nhân vật nhận: " + damage + " sát thương, HP còn: " + hp);

        knockback.GetKnockedBack(damageSource, 15f);
        StartCoroutine(flash.FlashRoutine());

        if (hp <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Debug.Log("Player đã chết");
    }
}