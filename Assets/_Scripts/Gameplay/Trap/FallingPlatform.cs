using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float disappearDelay = 2f;
    [SerializeField] private float respawnDelay = 2f;

    private Collider2D col;
    private SpriteRenderer sr;

    private bool isTriggered = false;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTriggered && collision.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
            StartCoroutine(PlatformRoutine());
        }
    }

    IEnumerator PlatformRoutine()
    {
        yield return new WaitForSeconds(disappearDelay);

        col.enabled = false;
        sr.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        col.enabled = true;
        sr.enabled = true;

        isTriggered = false;
    }
}