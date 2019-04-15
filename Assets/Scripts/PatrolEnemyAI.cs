using UnityEngine;
using UnityEngine.AI;
using StateConfig;

public class PatrolEnemyAI : MonoBehaviour
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


    public StateMachine<PatrolEnemyAI> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<PatrolEnemyAI>(this);
        stateMachine.ChangeState(PatrolState.Instance);
        agentNav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(agentNav.enabled)
            stateMachine.Update();
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
