using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private int dmg = 1;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 1f;

    [Header("Direction")]
    [SerializeField] private bool isFacingLeft = false;

    private void Start()
    {
        SetDirection(isFacingLeft ? -1f : 1f);

        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(float dir)
    {
        float direction = Mathf.Sign(dir);

        float angle = direction > 0 ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerHealth pl = col.GetComponent<PlayerHealth>();
        
            if (pl != null)
            {
                pl.TakeDamage(dmg, transform);
                Destroy(gameObject);
            }
        }
    }
}
