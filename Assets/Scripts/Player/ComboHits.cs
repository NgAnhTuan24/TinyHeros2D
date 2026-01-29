using UnityEngine;

public class ComboHits : MonoBehaviour
{
    public Animator anim;
    public int noOfKeyPresses = 0;
    public float maxComboDelay = 0;

    private float lastKeyPressedTime = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.time - lastKeyPressedTime > maxComboDelay)
        {
            noOfKeyPresses = 0;
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            lastKeyPressedTime = Time.time;
            noOfKeyPresses++;
            if(noOfKeyPresses == 1)
            {
                anim.SetBool("Attack1", true);
            }
            noOfKeyPresses = Mathf.Clamp(noOfKeyPresses, 0, 2);
        }
    }

    public void return1()
    {
        if (noOfKeyPresses >= 2)
        {
            anim.SetBool("Attack2", true);
        }
        else
        {
            anim.SetBool("Attack1", false);
            noOfKeyPresses = 0;
        }
    }

    public void return2()
    {
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        noOfKeyPresses = 0;
    }
}