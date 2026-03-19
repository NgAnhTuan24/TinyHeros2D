using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHp;
    private int hp;

    [SerializeField] private float invincibleTime = .8f;
    private bool isInvincible;

    Knockback knockback;
    Flash flash;
    SpriteRenderer sprite;

    private Rigidbody2D rb;
    private Animator anim;
    private HeartUI heartUI;

    private Coroutine invincibleCoroutine;

    void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
        sprite = GetComponent<SpriteRenderer>();
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
        if (isInvincible) return;

        isInvincible = true;

        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);

        heartUI.UpdateHearts(hp);

        Debug.Log("Nhân vật nhận: " + damage + " sát thương, HP còn: " + hp);

        StartCoroutine(DamageRoutine(damageSource));

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

    public void OnDie()
    {
        Destroy(gameObject);
    }

    private IEnumerator DamageRoutine(Transform damageSource)
    {
        knockback.GetKnockedBack(damageSource, 15f);

        yield return StartCoroutine(flash.FlashRoutine());

        if (invincibleCoroutine != null)
            StopCoroutine(invincibleCoroutine);

        invincibleCoroutine = StartCoroutine(InvincibleRoutine());
    }

    private IEnumerator InvincibleRoutine()
    {
        isInvincible = true;

        SetAlpha(0.5f);

        yield return new WaitForSeconds(invincibleTime);

        SetAlpha(1f);
        isInvincible = false;
    }

    private void SetAlpha(float alpha)
    {
        Color color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }
}