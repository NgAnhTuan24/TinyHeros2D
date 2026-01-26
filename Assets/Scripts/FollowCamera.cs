using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform cam;

    private float offsetY;
    private float offsetZ;
    
    void Start()
    {
        offsetY = transform.position.y;
        offsetZ = transform.position.z;
    }

    void Update()
    {
        transform.position = new Vector3(cam.position.x, offsetY, offsetZ);
    }
}
