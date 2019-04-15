using UnityEngine;
using StateConfig;

public class ChaseState1 : State<PatrolChaseEnemyAI>
{
    private static ChaseState1 instance;

    private ChaseState1()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static ChaseState1 Instance
    {
        get
        {
            if (instance == null)
            {
                new ChaseState1();
            }

            return instance;
        }
    }

    public override void EnterState(PatrolChaseEnemyAI agent)
    {
        Debug.Log("Entering Chase State");
    }

    public override void ExitState(PatrolChaseEnemyAI agent)
    {
        Debug.Log("Exiting Chase State");
    }

    public override void UpdateState(PatrolChaseEnemyAI agent)
    {
        agent.agentNav.SetDestination(agent.chasePlayer.position);
    }
}
