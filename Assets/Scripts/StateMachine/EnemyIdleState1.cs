using UnityEngine;
using StateConfig;

public class EnemyIdleState1 : State<IdleShootEnemyAI1>
{
    private static EnemyIdleState1 instance;
    public bool canSee;

    private EnemyIdleState1()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static EnemyIdleState1 Instance
    {
        get
        {
            if (instance == null)
            {
                new EnemyIdleState1();
            }

            return instance;
        }
    }

    public override void EnterState(IdleShootEnemyAI1 agent)
    {
        Debug.Log("Entering Idle State");
    }

    public override void ExitState(IdleShootEnemyAI1 agent)
    {
        Debug.Log("Exiting Idle State");
    }

    public override void UpdateState(IdleShootEnemyAI1 agent)
    {

        canSee = CanSeePlayer(agent);
        if (canSee)
        {
            agent.stateMachine.ChangeState(ShootState1.Instance);
        }

    }
    public bool CanSeePlayer(IdleShootEnemyAI1 agent)
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

