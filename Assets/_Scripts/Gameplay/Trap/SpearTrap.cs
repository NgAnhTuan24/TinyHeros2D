using UnityEngine;
using System.Collections;

public class SpearTrap : MonoBehaviour
{
    [SerializeField] private float idleTime = 2f;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(TrapLoop());
    }

    IEnumerator TrapLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(idleTime);

            anim.SetTrigger("Attack");
        }
    }
}