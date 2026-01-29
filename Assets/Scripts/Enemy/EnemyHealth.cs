using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp;

    private int hp;
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        hp = maxHp;    
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        //hp = Mathf.Clamp(hp, 0, maxHp);

        Debug.Log("Quái nhận: " + damage + " sát thương, máu hiện tại: " + hp);

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
