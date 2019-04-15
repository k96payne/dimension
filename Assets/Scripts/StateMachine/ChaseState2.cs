using UnityEngine;
using StateConfig;

public class ChaseState2 : State<PatrolChaseEnemyAI1>
{
    private static ChaseState2 instance;

    private ChaseState2()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static ChaseState2 Instance
    {
        get
        {
            if (instance == null)
            {
                new ChaseState2();
            }

            return instance;
        }
    }

    public override void EnterState(PatrolChaseEnemyAI1 agent)
    {
        Debug.Log("Entering Chase State");
    }

    public override void ExitState(PatrolChaseEnemyAI1 agent)
    {
        Debug.Log("Exiting Chase State");
    }

    public override void UpdateState(PatrolChaseEnemyAI1 agent)
    {
        agent.agentNav.SetDestination(agent.chasePlayer.position);
    }
}
