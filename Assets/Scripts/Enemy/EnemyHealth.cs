using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp;

    private int hp;

    private void Start()
    {
        hp = maxHp;    
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        //hp = Mathf.Clamp(hp, 0, maxHp);

        Debug.Log("Quái nhận: " + damage + " sát thương, máu hiện tại: " + hp);

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
