using UnityEngine;

public class ComboHits : MonoBehaviour
{
    Character player;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.2f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int noOfKeyPresses = 0;
    [SerializeField] private float maxComboDelay = 0;

    private float lastKeyPressedTime = 0;
    
    Animator anim;

    private void Start()
    {
        player = Init.player;
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
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayer);

        if (hit != null)
        {
            hit.GetComponent<EnemyHealth>()?.TakeDamage(player.Damage, transform);
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