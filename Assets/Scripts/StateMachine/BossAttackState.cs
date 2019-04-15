using UnityEngine;
using StateConfig;

public class BossAttackState : State<BossAI>
{
    private static BossAttackState instance;
    private bool recentlyShot = false;
    public float projDelay = 3.0f;

    private BossAttackState()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static BossAttackState Instance
    {
        get
        {
            if (instance == null)
            {
                new BossAttackState();
            }

            return instance;
        }
    }

    public override void EnterState(BossAI agent)
    {
        Debug.Log("Entering BossAttack State");
    }

    public override void ExitState(BossAI agent)
    {
        Debug.Log("Exiting BossAttack State");
    }

    public override void UpdateState(BossAI agent)
    {
        rotateBeforeAttack(agent);
        agent.agentNav.SetDestination(agent.chasePlayer2.position);
        Shoot(agent);
    }

    void Shoot(BossAI agent)
    {
        if (!recentlyShot)
        {
            GameObject projPrefab1 = Object.Instantiate(agent.projectile, agent.lineOfSight1.position, agent.projectile.transform.rotation);
            Rigidbody projectile1 = projPrefab1.GetComponent<Rigidbody>();
            projectile1.rotation = Quaternion.LookRotation(agent.lineOfSight1.forward);
            projectile1.velocity = agent.lineOfSight1.forward * agent.shootSpeed;
            Object.Destroy(projPrefab1, 3.0f);

            GameObject projPrefab2 = Object.Instantiate(agent.projectile, agent.lineOfSight2.position, agent.projectile.transform.rotation);
            Rigidbody projectile2 = projPrefab2.GetComponent<Rigidbody>();
            projectile2.rotation = Quaternion.LookRotation(agent.lineOfSight2.forward);
            projectile2.velocity = agent.lineOfSight2.forward * agent.shootSpeed;
            Object.Destroy(projPrefab2, 3.0f);

            GameObject projPrefab3 = Object.Instantiate(agent.projectile, agent.lineOfSight3.position, agent.projectile.transform.rotation);
            Rigidbody projectile3 = projPrefab3.GetComponent<Rigidbody>();
            projectile3.rotation = Quaternion.LookRotation(agent.lineOfSight3.forward);
            projectile3.velocity = agent.lineOfSight3.forward * agent.shootSpeed;
            Object.Destroy(projPrefab3, 3.0f);
            recentlyShot = true;

        }
        else
        {
            if (projDelay > 0f)
            {
                agent.agentNav.enabled = true;
                projDelay -= Time.deltaTime;
            }
            if (projDelay <= 0f)
            {
                agent.agentNav.enabled = false;
                recentlyShot = false;
                projDelay = 3.0f;
            }
        }
    }
    void rotateBeforeAttack(BossAI agent)
    {
        if(projDelay <= 1.0f)
        {
            //agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, agent.transform.rotation, 360);
           
            agent.transform.Rotate(Vector3.up, 6.85f);
         
        }
    }
}
