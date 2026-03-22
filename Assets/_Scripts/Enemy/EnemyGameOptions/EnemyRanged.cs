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

        yield return new WaitForSeconds(0.6f);
    }

    public void SpawnProjectile()
    {
    }
}