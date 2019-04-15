using UnityEngine;
using StateConfig;

public class EnemyIdleState2 : State<IdleChaseEnemyAI>
{
    private static EnemyIdleState2 instance;
    public bool canSee;

    private EnemyIdleState2()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static EnemyIdleState2 Instance
    {
        get
        {
            if (instance == null)
            {
                new EnemyIdleState2();
            }

            return instance;
        }
    }

    public override void EnterState(IdleChaseEnemyAI agent)
    {
        Debug.Log("Entering Idle State");
    }

    public override void ExitState(IdleChaseEnemyAI agent)
    {
        Debug.Log("Exiting Idle State");
    }

    public override void UpdateState(IdleChaseEnemyAI agent)
    {

        canSee = CanSeePlayer(agent);
        if (canSee)
        {
            agent.stateMachine.ChangeState(ChaseState.Instance);
        }

    }
    public bool CanSeePlayer(IdleChaseEnemyAI agent)
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
            if (Physics.Raycast(agent.transform.position, rayDirection, out hit, 11))
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

