using UnityEngine;
using StateConfig;

public class ShootState : State<IdleShootEnemyAI>
{
    private static ShootState instance;
    private bool recentlyShot = false;
    private float projDelay = 2.0f;

    private ShootState()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static ShootState Instance
    {
        get
        {
            if (instance == null)
            {
                new ShootState();
            }

            return instance;
        }
    }

    public override void EnterState(IdleShootEnemyAI agent)
    {
        Debug.Log("Entering Shoot State");
    }

    public override void ExitState(IdleShootEnemyAI agent)
    {
        Debug.Log("Exiting Shoot State");
    }

    public override void UpdateState(IdleShootEnemyAI agent)
    {
        agent.agentNav.SetDestination(agent.chasePlayer.position);
        Shoot(agent);
    }

    void Shoot(IdleShootEnemyAI agent)
    {
        if(!recentlyShot)
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
