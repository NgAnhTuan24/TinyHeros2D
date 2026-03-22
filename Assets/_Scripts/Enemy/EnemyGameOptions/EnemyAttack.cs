using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator DoAttack()
    {
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);
    }

    public void DealDamage()
    {
        Debug.Log("Hit Player");
    }
}