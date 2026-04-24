using UnityEngine;

public class PlatformCarry : MonoBehaviour
{
    private Transform player;
    private Vector3 lastPos;

    private void Start()
    {
        lastPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            player = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            player = null;
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 delta = transform.position - lastPos;
            player.position += delta;
        }

        lastPos = transform.position;
    }
}