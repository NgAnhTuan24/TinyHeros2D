using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public PlayerMovement movement;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }
}
