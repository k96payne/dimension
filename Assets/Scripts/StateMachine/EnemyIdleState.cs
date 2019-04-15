using UnityEngine;
using StateConfig;

public class EnemyIdleState : State<IdleShootEnemyAI>
{
    private static EnemyIdleState instance;
    public bool canSee;

    private EnemyIdleState()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static EnemyIdleState Instance
    {
        get
        {
            if (instance == null)
            {
                new EnemyIdleState();
            }

            return instance;
        }
    }

    public override void EnterState(IdleShootEnemyAI agent)
    {
        Debug.Log("Entering Idle State");
    }

    public override void ExitState(IdleShootEnemyAI agent)
    {
        Debug.Log("Exiting Idle State");
    }

    public override void UpdateState(IdleShootEnemyAI agent)
    {

        canSee = CanSeePlayer(agent);
        if (canSee)
        {
            agent.stateMachine.ChangeState(ShootState.Instance);
        }

    }
    public bool CanSeePlayer(IdleShootEnemyAI agent)
    {
        RaycastHit hit;
        Vector3 rayDirection = GameObject.Find("Player").transform.position - agent.transform.position;

        if (Physics.Raycast(agent.lineOfSight1.position, agent.lineOfSight1.forward, out hit, 7))
        {
            if (hit.transform.tag == "Player")
            {
                agent.chasePlayer = hit.transform;
                return true;
            }
        }

        if ((Vector3.Angle(rayDirection, agent.transform.forward)) < 160.0f)
        {
            if (Physics.Raycast(agent.transform.position, rayDirection, out hit, 12))
            {

                if (hit.transform.tag == "Player")
                {
                    agent.chasePlayer = hit.transform;
                    return true;
                }
            }
        }
        return false;
    }
}
