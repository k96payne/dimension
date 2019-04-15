using UnityEngine;
using StateConfig;

public class BossIdleState : State<BossAI>
{
    private static BossIdleState instance;
    public bool canSee;

    private BossIdleState()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static BossIdleState Instance
    {
        get
        {
            if (instance == null)
            {
                new BossIdleState();
            }

            return instance;
        }
    }

    public override void EnterState(BossAI agent)
    {
        Debug.Log("Entering Idle State");
    }

    public override void ExitState(BossAI agent)
    {
        Debug.Log("Exiting Idle State");
    }

    public override void UpdateState(BossAI agent)
    {
    
        canSee = CanSeePlayer(agent);
        if (canSee)
        {
            agent.stateMachine.ChangeState(BossAttackState.Instance);
        }

    }
    public bool CanSeePlayer(BossAI agent)
    {
        RaycastHit hit;
        Vector3 rayDirection = GameObject.Find("Player").transform.position - agent.transform.position;

        if (Physics.Raycast(agent.lineOfSight1.position, agent.lineOfSight1.forward, out hit, 5))
        {
            if (hit.transform.tag == "Player")
            {
                agent.chasePlayer2 = hit.transform;
                return true;
            }
        }

        if ((Vector3.Angle(rayDirection, agent.transform.forward)) < 135.0f)
        {
            if (Physics.Raycast(agent.transform.position, rayDirection, out hit, 10))
            {

                if (hit.transform.tag == "Player")
                {
                    agent.chasePlayer2 = hit.transform;
                    return true;
                }
            }
        }
        return false;
    }
}
