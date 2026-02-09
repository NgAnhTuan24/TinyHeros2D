using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/Data", order = 50)]
public class EnemyData : ScriptableObject
{
    public EnemyType enemyType;

    [Header("Stats")]
    public int maxHp;
    public float moveSpeed;
    public float gravityScale = 5f;

    [Header("Detection")]
    public float chaseRange;
    public float attackRange;

    [Header("Attack")]
    public float attackCooldown;
    public int attackDmg = 1;
    public float attackHitRadius;
}
