using UnityEngine;

public class CharacterFollowCamera : MonoBehaviour
{
    public Transform cam;
    public float offsetX = -4f; // lệch về bên trái
    public bool lockY = true;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 targetPos = new Vector2(
            cam.position.x + offsetX,
            lockY ? rb.position.y : cam.position.y
        );

        rb.MovePosition(targetPos);
    }
}
