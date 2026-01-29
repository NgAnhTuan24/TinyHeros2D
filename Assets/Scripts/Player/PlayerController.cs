using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Character player;

    void Start()
    {
        player = Init.player;
    }

    void Update()
    {
        player.Update();
    }

    void FixedUpdate()
    {
        player.FixedUpdate();
    }
}