using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp;
    private int hp;

    Knockback knockback;
    Flash flash;

    private Rigidbody2D rb;
    private Animator anim;
    private HeartUI heartUI;

    void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void SetHeartUI(HeartUI ui)
    {
        heartUI = ui;

        hp = maxHp;
        heartUI.Init(maxHp);
        heartUI.UpdateHearts(hp);
    }

    public void TakeDamage(int damage , Transform damageSource)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);

        heartUI.UpdateHearts(hp);

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

        if (!rb.simulated) return;

        rb.velocity = Vector2.zero;
        rb.simulated = false;

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }

        anim.SetTrigger("IsDie");
    }
}