using UnityEngine;
using UnityEngine.AI;
using StateConfig;

public class BossAI : MonoBehaviour
{
    public int health;
    public NavMeshAgent agentNav;
    public Transform lineOfSight1;
    public Transform lineOfSight2;
    public Transform lineOfSight3;
    public GameObject projectile;
    public float shootSpeed;
    private RaycastHit hit;

    [HideInInspector] public int nextPatrolPoint;
    [HideInInspector] public Transform chasePlayer2;
    [HideInInspector] public float stateTimeElapsed = 0;


    public StateMachine<BossAI> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<BossAI>(this);
        stateMachine.ChangeState(BossIdleState.Instance);
        agentNav = GetComponent<NavMeshAgent>();
        //lineOfSight = GameObject.Find("lineOfSight2").transform;
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
