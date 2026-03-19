using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStateMachine StateMachine { get; private set; }

    public EnemyIdleState IdleState { get; private set; }
    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float detectRange;

    public EnemyMovement Movement { get; private set; }
    public EnemyDetection Detection { get; private set; }
    public EnemyCombat Combat { get; private set; }
    public EnemyAnimator Animator { get; private set; }

    [SerializeField] private bool usePatrol = true;
    public bool UsePatrol => usePatrol;

    private void Awake()
    {
        Movement = GetComponent<EnemyMovement>();
        Detection = GetComponent<EnemyDetection>();
        Combat = GetComponent<EnemyCombat>();
        Animator = GetComponent<EnemyAnimator>();

        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this);
        PatrolState = new EnemyPatrolState(this);
        ChaseState = new EnemyChaseState(this);
        AttackState = new EnemyAttackState(this);
    }

    void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        StateMachine.CurrentState?.Update();
    }

    public float MoveSpeed => moveSpeed;
    public float ChaseSpeed => chaseSpeed;
    public float AttackRange => attackRange;
    public float DetectRange => detectRange;
}
