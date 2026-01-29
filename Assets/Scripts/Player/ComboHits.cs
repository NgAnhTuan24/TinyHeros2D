using UnityEngine;

public class ComboHits : MonoBehaviour
{
    public Animator anim;
    public int noOfClicks = 0;
    public float maxComboDelay = 0;

    private float lastClickedTime = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if(Input.GetMouseButtonDown(0))
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            if(noOfClicks == 1)
            {
                anim.SetBool("Attack1", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 2);
        }
    }

    public void return1()
    {
        if (noOfClicks >= 2)
        {
            anim.SetBool("Attack2", true);
        }
        else
        {
            anim.SetBool("Attack1", false);
            noOfClicks = 0;
        }
    }

    public void return2()
    {
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        noOfClicks = 0;
    }
}