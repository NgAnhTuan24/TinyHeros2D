using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable, IEnemy
{
    public event Action OnDie;

    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [SerializeField] private EnemyHealthUI healthBar;

    [SerializeField] private float knockbackThrust;
    [SerializeField] private GameObject deathVFX;

    [SerializeField] private bool isBoss = false;
    [SerializeField] private int chapterID;

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
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage, Transform damageSource)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.SetHealth(currentHealth);

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

        OnDie?.Invoke();

        if (isBoss)
        {
            UnlockNextChapter();
        }

        Instantiate(deathVFX, transform.position, Quaternion.identity);

        drop.Drop();

        Destroy(gameObject);
    }

    void UnlockNextChapter()
    {
        int nextChapter = chapterID + 1;

        if (!GameManager.instance.unlockedChapters.Contains(nextChapter))
        {
            GameManager.instance.unlockedChapters.Add(nextChapter);

            Debug.Log("Mở khóa chapter " + nextChapter);

            GameManager.instance.SaveGame();
        }
    }
}
