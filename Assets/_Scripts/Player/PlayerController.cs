using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public PlayerMovement movement;

    public PlayerHealth health;

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

        movement = GetComponent<PlayerMovement>();

        health = GetComponent<PlayerHealth>();
    }
}
