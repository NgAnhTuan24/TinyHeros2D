using UnityEngine;

public class ThrowHits : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public Transform point;

    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetBool("IsThrow", true);
        }    
    }

    public void GoToBackIdle()
    {
        anim.SetBool("IsThrow", false);
    }
    
    public void ThrowStone()
    {
        GameObject stone = Instantiate(projectilePrefab, point.position, Quaternion.identity);

        Rigidbody2D rb = stone.GetComponent<Rigidbody2D>();

        if (rb != null )
        {
            float dir = transform.localScale.x > 0 ? 1f : -1f;
            rb.velocity = Vector2.right * dir * projectileSpeed;
        }
    }
}
