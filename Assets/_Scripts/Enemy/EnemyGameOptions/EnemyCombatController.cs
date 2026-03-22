using System.Collections;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    [Header("Detection Range")]
    public float meleeRange = 1.5f;
    public float dashRange = 4f;
    public float rangedRange = 7f;

    [Header("Layer")]
    public LayerMask playerLayer;

    [Header("Cooldown")]
    public float dashCooldown = 3f;
    public float meleeCooldown = 2f;
    public float rangedCooldown = 3f;

    [Header("Facing")]
    public bool startFacingRight = true;

    private float lastDashTime;
    private float lastMeleeTime;
    private float lastRangedTime;

    private bool isActing;

    private bool isMelee;
    private bool isDash;
    private bool isRanged;

    private Transform player;

    private EnemyDash dash;
    private EnemyAttack attack;
    private EnemyRanged ranged;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        dash = GetComponent<EnemyDash>();
        attack = GetComponent<EnemyAttack>();
        ranged = GetComponent<EnemyRanged>();
    }

    void Start()
    {
        SetFacing(startFacingRight);
    }

    void Update()
    {
        FacePlayer();
        DetectPlayer();

        if (isActing) return;

        if (isMelee && Time.time >= lastMeleeTime + meleeCooldown)
        {
            StartCoroutine(MeleeAttack());
            return;
        }

        if (isDash && Time.time >= lastDashTime + dashCooldown)
        {
            StartCoroutine(DashAttack());
            return;
        }

        if (isRanged && Time.time >= lastRangedTime + rangedCooldown)
        {
            StartCoroutine(RangedAttack());
            return;
        }
    }

    void DetectPlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, rangedRange, playerLayer);
        Debug.DrawRay(transform.position, dir * rangedRange, Color.red);

        if (hit.collider != null)
        {
            isMelee = false;
            isDash = false;
            isRanged = false;

            if (distance <= meleeRange)
                isMelee = true;
            else if (distance <= dashRange)
                isDash = true;
            else if (distance <= rangedRange)
                isRanged = true;
        }
        else
        {
            isMelee = isDash = isRanged = false;
        }
    }

    IEnumerator DashAttack()
    {
        isActing = true;

        lastDashTime = Time.time;

        yield return dash.DoDash();
        yield return attack.DoAttack();

        isActing = false;
    }

    IEnumerator MeleeAttack()
    {
        isActing = true;

        lastMeleeTime = Time.time;

        yield return attack.DoAttack();

        isActing = false;
    }

    IEnumerator RangedAttack()
    {
        isActing = true;

        lastRangedTime = Time.time;

        yield return ranged.DoRangedAttack();

        isActing = false;
    }

    void SetFacing(bool faceRight)
    {
        if (faceRight)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void FacePlayer()
    {
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, dashRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangedRange);
    }
}