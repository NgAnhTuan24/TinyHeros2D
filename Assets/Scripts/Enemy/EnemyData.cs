using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/Data", order = 50)]
public class EnemyData : ScriptableObject
{
    public int maxHp = 3;
    public float moveSpeed = 3f;
    public float gravityScale = 5f;

    public float chaseRange = 6f;
    public float attackRange = 1.2f;

    public float attackCooldown = 2f;
    public int attackDmg = 1;
    public float attackHitRadius = 0.3f;
}
