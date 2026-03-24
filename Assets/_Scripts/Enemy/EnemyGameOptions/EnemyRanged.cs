using System.Collections;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator DoRangedAttack()
    {
        animator.SetTrigger("AttackRange");

        yield return new WaitForSeconds(3f);
    }

    public void SpawnProjectile()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Projectile projectile = proj.GetComponent<Projectile>();

        if (projectile != null)
        {
            float dir = transform.localScale.x > 0 ? 1f : -1f;
            projectile.Init(dir);
        }
    }
}