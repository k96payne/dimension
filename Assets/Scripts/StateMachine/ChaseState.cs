using UnityEngine;
using StateConfig;

public class ChaseState : State<IdleChaseEnemyAI>
{
    private static ChaseState instance;

    private ChaseState()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static ChaseState Instance
    {
        get
        {
            if (instance == null)
            {
                new ChaseState();
            }

            return instance;
        }
    }

    public override void EnterState(IdleChaseEnemyAI agent)
    {
        Debug.Log("Entering Chase State");
    }

    public override void ExitState(IdleChaseEnemyAI agent)
    {
        Debug.Log("Exiting Chase State");
    }

    public override void UpdateState(IdleChaseEnemyAI agent)
    {
        agent.agentNav.SetDestination(agent.chasePlayer.position);
    }
}
