using System.Collections;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackDelay = 2f;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(AttackLoop());
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            animator.SetTrigger("Attack");

            yield return new WaitForSeconds(attackDelay);
        }
    }

    public void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

        Arrow arrowScript = arrow.GetComponent<Arrow>();

        if (arrowScript != null)
        {
            float dir = transform.localScale.x > 0 ? 1f : -1f;
            arrowScript.SetDirection(dir);
        }
    }
}
