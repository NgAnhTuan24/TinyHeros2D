using UnityEngine;

public class AutoMoveCamera : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
