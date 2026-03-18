using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool gettingKnockedBack { get; private set; }

    [SerializeField] private float knockBackTime = .3f;

    private Rigidbody2D rb;
    private Coroutine knockCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        if (gettingKnockedBack) return;

        gettingKnockedBack = true;
        Vector2 direction = (transform.position - damageSource.position).normalized;
        direction.y = 0.5f;
        direction.Normalize();

        rb.velocity = Vector2.zero;
        rb.AddForce(direction * knockBackThrust, ForceMode2D.Impulse);

        if (knockCoroutine != null)
            StopCoroutine(knockCoroutine);

        knockCoroutine = StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        gettingKnockedBack = false;
    }
}