using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public PlayerMovement movement;
    public ComboHits hits;
    public ThrowHits throwHits;

    private void Awake()
    {
        instance = this;
    }

    public void RegisterPlayer(GameObject player)
    {
        movement = player.GetComponent<PlayerMovement>();
        hits = player.GetComponent<ComboHits>();
        throwHits = player.GetComponent<ThrowHits>();
    }

    public void MoveLeft()
    {
        movement?.MoveLeft();
    }

    public void MoveRight()
    {
        movement?.MoveRight();
    }

    public void StopMove()
    {
        movement?.StopMove();
    }

    public void Jump()
    {
        movement?.JumpButton();
    }

    // attack
    public void Attack()
    {
        hits?.AttackButton();
    }

    // throw
    public void Throw()
    {
        throwHits?.ThrowButton();
    }
}
