using UnityEngine;

public class ComboHits : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.2f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int noOfKeyPresses = 0;
    [SerializeField] private float maxComboDelay = 0;

    [Header("Damage")]
    [SerializeField] private int minDmg = 1;
    [SerializeField] private int maxDmg = 3;
    [SerializeField] private float critChance = .25f;
    [SerializeField] private float critMultiplier = 2f;

    private float lastKeyPressedTime = 0;
    
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.time - lastKeyPressedTime > maxComboDelay)
        {
            noOfKeyPresses = 0;
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            lastKeyPressedTime = Time.time;
            noOfKeyPresses++;
            if(noOfKeyPresses == 1)
            {
                anim.SetBool("Attack1", true);
            }
            noOfKeyPresses = Mathf.Clamp(noOfKeyPresses, 0, 2);
        }
    }

    public void DealDamage()
    {
        // Chỉ đánh gây sát thương được với 1 enemy -> cần mở rộng là gây lên nhiều quái 
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayer);

        if (hit != null)
        {
            int dmg = Random.Range(minDmg, maxDmg + 1);

            if (Random.value < critChance)
            {
                dmg = Mathf.RoundToInt(dmg * critMultiplier);
            }

            hit.GetComponent<EnemyHealth>()?.TakeDamage(dmg, transform);
        }
    }

    public void return1()
    {
        if (noOfKeyPresses >= 2)
        {
            anim.SetBool("Attack2", true);
        }
        else
        {
            anim.SetBool("Attack1", false);
            noOfKeyPresses = 0;
        }
    }

    public void return2()
    {
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        noOfKeyPresses = 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}