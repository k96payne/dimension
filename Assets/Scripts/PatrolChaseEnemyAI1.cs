using UnityEngine;
using UnityEngine.AI;
using StateConfig;

public class PatrolChaseEnemyAI1 : MonoBehaviour
{
    public int health;
    public Transform[] patrolPoints;
    public NavMeshAgent agentNav;
    public Transform lineOfSight;
    public GameObject projectile;
    public float shootSpeed;
    private RaycastHit hit;

    [HideInInspector] public int nextPatrolPoint;
    [HideInInspector] public Transform chasePlayer;
    [HideInInspector] public float stateTimeElapsed = 0;


    public StateMachine<PatrolChaseEnemyAI1> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<PatrolChaseEnemyAI1>(this);
        stateMachine.ChangeState(PatrolState2.Instance);
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
