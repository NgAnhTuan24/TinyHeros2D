using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public Action onDie;

    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [SerializeField] private float knockbackThrust;
    [SerializeField] private GameObject deathVFX;

    Flash flash;
    Knockback knockback;
    EnemyDrop drop;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        drop = GetComponent<EnemyDrop>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, Transform damageSource)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        knockback.GetKnockedBack(damageSource, knockbackThrust);
        StartCoroutine(flash.FlashRoutine());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Quái đã chết");

        onDie?.Invoke();

        Instantiate(deathVFX, transform.position, Quaternion.identity);

        Destroy(gameObject);

        drop.Drop();
    }
}
