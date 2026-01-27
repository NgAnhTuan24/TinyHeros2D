using UnityEngine;

public class Pink : Character
{
    public Pink(GameObject gameObject) : base(gameObject)
    {
        speed = 5;
    }

    protected override void InputHandle()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < -0.01f)
        {
            rb.velocity = new Vector2(-speed, 0);
            direction = Direction.Left;
        }
        else if (horizontalInput > 0.01f)
        {
            rb.velocity = new Vector2(speed, 0);
            direction = Direction.Right;
        }
    }
}
