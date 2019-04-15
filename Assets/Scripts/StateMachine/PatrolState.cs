using UnityEngine;
using StateConfig;

public class PatrolState : State<PatrolEnemyAI>
{
    private static PatrolState instance;

    private PatrolState()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;
    }

    public static PatrolState Instance
    {
        get
        {
            if(instance == null)
            {
                new PatrolState();
            }

            return instance;
        }
    }

    public override void EnterState(PatrolEnemyAI agent)
    {
        Debug.Log("Entering Patrol State");
    }

    public override void ExitState(PatrolEnemyAI agent)
    {
        Debug.Log("Exiting Patrol State");
    }

    public override void UpdateState(PatrolEnemyAI agent)
    {

        agent.agentNav.destination = agent.patrolPoints[agent.nextPatrolPoint].position;

        if (agent.agentNav.remainingDistance <= agent.agentNav.stoppingDistance && !agent.agentNav.pathPending)
        {
           agent.nextPatrolPoint = (agent.nextPatrolPoint + 1) % agent.patrolPoints.Length;
        }


    }
}
