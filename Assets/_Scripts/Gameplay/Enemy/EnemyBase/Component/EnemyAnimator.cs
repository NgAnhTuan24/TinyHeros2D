using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetMove(bool value)
    {
        anim.SetBool("isMoving", value);
    }

    public void TriggerAttack()
    {
        anim.SetTrigger("Attack");
    }
}
