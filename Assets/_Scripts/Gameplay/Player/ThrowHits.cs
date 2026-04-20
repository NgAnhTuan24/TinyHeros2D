using UnityEngine;

public class ThrowHits : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform point;

    [SerializeField] private AudioClip throwSFX;

    Animator anim;

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

    //UI Button Call
    public void ThrowButton()
    {
        anim.SetBool("IsThrow", true);
        //AudioManager.instance.PlaySFX(throwSFX);
    }

    public void PlayThrowSFX()
    {
        AudioManager.instance.PlaySFX(throwSFX);
    }

    public void GoToBackIdle()
    {
        anim.SetBool("IsThrow", false);
    }
    
    public void ThrowStone()
    {
        GameObject proj = Instantiate(projectilePrefab, point.position, Quaternion.identity);

        Projectile projectile = proj.GetComponent<Projectile>();

        if (projectile != null )
        {
            float dir = transform.localScale.x > 0 ? 1f : -1f;
            projectile.Init(dir);
        }
    }
}
