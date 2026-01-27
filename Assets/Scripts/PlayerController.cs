using System.Collections;
using System.Collections.Generic;
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
}
