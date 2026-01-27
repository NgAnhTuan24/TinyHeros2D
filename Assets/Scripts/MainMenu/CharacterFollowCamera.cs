using UnityEngine;

public class CharacterFollowCamera : MonoBehaviour
{
    public Transform cam;
    public float offsetX = -8f;

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            cam.position.x + offsetX,
            transform.position.y,
            transform.position.z
        );
    }
}
