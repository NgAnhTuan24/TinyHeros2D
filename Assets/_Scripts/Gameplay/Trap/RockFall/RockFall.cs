using UnityEngine;

public class RockFall : MonoBehaviour
{
    [SerializeField] private float fallDelay = 0.3f;

    private Rigidbody2D rb;
    private bool isFalling = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    public void TriggerFall()
    {
        if (isFalling) return;

        isFalling = true;
        Invoke(nameof(StartFalling), fallDelay);
    }

    void StartFalling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}