using UnityEngine;
using StateConfig;

public class PatrolState3 : State<PatrolChaseEnemyAI2>
{
    private static PatrolState3 instance;
    public bool canSee;

    private PatrolState3()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static PatrolState3 Instance
    {
        get
        {
            if (instance == null)
            {
                new PatrolState3();
            }

            return instance;
        }
    }

    public override void EnterState(PatrolChaseEnemyAI2 agent)
    {
        Debug.Log("Entering Patrol State1");
    }

    public override void ExitState(PatrolChaseEnemyAI2 agent)
    {
        Debug.Log("Exiting Patrol State1");
    }

    public override void UpdateState(PatrolChaseEnemyAI2 agent)
    {

        agent.agentNav.destination = agent.patrolPoints[agent.nextPatrolPoint].position;

        if (agent.agentNav.remainingDistance <= agent.agentNav.stoppingDistance && !agent.agentNav.pathPending)
        {
            agent.nextPatrolPoint = (agent.nextPatrolPoint + 1) % agent.patrolPoints.Length;
        }

        canSee = CanSeePlayer(agent);
        if (canSee)
        {

            agent.stateMachine.ChangeState(ChaseState3.Instance);
        }

    }
    public bool CanSeePlayer(PatrolChaseEnemyAI2 agent)
    {

        RaycastHit hit;
        Vector3 rayDirection = GameObject.Find("Player").transform.position - agent.transform.position;

        if (Physics.Raycast(agent.lineOfSight.position, agent.lineOfSight.forward, out hit, 5))
        {
            if (hit.transform.tag == "Player")
            {
                agent.chasePlayer = hit.transform;
                return true;
            }
        }

        if ((Vector3.Angle(rayDirection, agent.transform.forward)) < 160.0f)
        {
            if (Physics.Raycast(agent.transform.position, rayDirection, out hit, 10))
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
