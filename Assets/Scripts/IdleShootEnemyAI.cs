using UnityEngine;
using UnityEngine.AI;
using StateConfig;

public class IdleShootEnemyAI : MonoBehaviour
{
    public int health;
    public Transform[] patrolPoints;
    public NavMeshAgent agentNav;
    public Transform lineOfSight1;
    public GameObject projectile;
    public float shootSpeed;
    private RaycastHit hit;

    [HideInInspector] public int nextPatrolPoint;
    [HideInInspector] public Transform chasePlayer;
    [HideInInspector] public float stateTimeElapsed = 0;


    public StateMachine<IdleShootEnemyAI> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<IdleShootEnemyAI>(this);
        stateMachine.ChangeState(EnemyIdleState.Instance);
        agentNav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agentNav.enabled)
        {
            stateMachine.Update();
        }
    }

    void Awake()
    {
        agentNav = GetComponent<NavMeshAgent>();
    }

    public void DamageEnemy()
    {
        if (health == 0)
        {
            Destroy(gameObject, 0);
        }
        else
        {
            health--;
        }
    }

}
