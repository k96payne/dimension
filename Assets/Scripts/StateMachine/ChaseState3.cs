using UnityEngine;
using StateConfig;

public class ChaseState3 : State<PatrolChaseEnemyAI2>
{
    private static ChaseState3 instance;

    private ChaseState3()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static ChaseState3 Instance
    {
        get
        {
            if (instance == null)
            {
                new ChaseState3();
            }

            return instance;
        }
    }

    public override void EnterState(PatrolChaseEnemyAI2 agent)
    {
        Debug.Log("Entering Chase State");
    }

    public override void ExitState(PatrolChaseEnemyAI2 agent)
    {
        Debug.Log("Exiting Chase State");
    }

    public override void UpdateState(PatrolChaseEnemyAI2 agent)
    {
        agent.agentNav.SetDestination(agent.chasePlayer.position);
    }
}
