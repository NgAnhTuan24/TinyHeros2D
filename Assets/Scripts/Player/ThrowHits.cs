using UnityEngine;

public class ThrowHits : MonoBehaviour
{
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
}
