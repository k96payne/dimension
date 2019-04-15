using UnityEngine;
using StateConfig;

public class ShootState1 : State<IdleShootEnemyAI1>
{
    private static ShootState1 instance;
    private bool recentlyShot = false;
    private float projDelay = 2.0f;

    private ShootState1()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static ShootState1 Instance
    {
        get
        {
            if (instance == null)
            {
                new ShootState1();
            }

            return instance;
        }
    }

    public override void EnterState(IdleShootEnemyAI1 agent)
    {
        Debug.Log("Entering Shoot State1");
    }

    public override void ExitState(IdleShootEnemyAI1 agent)
    {
        Debug.Log("Exiting Shoot State1");
    }

    public override void UpdateState(IdleShootEnemyAI1 agent)
    {
        agent.agentNav.SetDestination(agent.chasePlayer.position);
        Shoot1(agent);
    }

    void Shoot1(IdleShootEnemyAI1 agent)
    {
        if (!recentlyShot)
        {
            GameObject projPrefab = Object.Instantiate(agent.projectile, agent.lineOfSight1.position, agent.projectile.transform.rotation);
            Rigidbody projectile = projPrefab.GetComponent<Rigidbody>();
            projectile.rotation = Quaternion.LookRotation(agent.lineOfSight1.forward);
            projectile.velocity = agent.lineOfSight1.forward * agent.shootSpeed;
            Object.Destroy(projPrefab, 2.0f);
            recentlyShot = true;
        }
        else
        {
            if (projDelay > 0f)
            {
                projDelay -= Time.deltaTime;
            }
            if (projDelay <= 0f)
            {
                recentlyShot = false;
                projDelay = 2.0f;
            }
        }

    }
}
