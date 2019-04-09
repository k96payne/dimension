using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public bool beenPunched = false;
    public bool kicked = false;
    public bool patrolOnly = false;
    public Vector3 direction;
    public int health = 3;

    public State currentState;
    public Transform[] patrolPoints;
    public NavMeshAgent agent;
    public Transform lineOfSight;
    public State remainState;
    public GameObject projectile;

    LayerMask enemyPlatform;

    [HideInInspector] public int nextPatrolPoint;
    [HideInInspector] public Transform chasePlayer;
    [HideInInspector] public float stateTimeElapsed = 0;

    void Awake()
    {
        enemyPlatform = LayerMask.GetMask("EnemySurface");
        chasePlayer = patrolPoints[0];
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (health <= 0)
            Destroy(gameObject, 0);

        if (agent.enabled && Physics.Raycast(transform.position, -Vector3.up, 0.3f, enemyPlatform))
            currentState.UpdateState(this);
    }

    void OnDrawGizmos()
    {
        if(currentState != null && lineOfSight != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(lineOfSight.position, 2);
        }
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState && !patrolOnly)
        {
            currentState = nextState;
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }

    public void DamageEnemyKick(Vector3 dir)
    {
        direction = dir * 400;
        StartCoroutine("Kick");
    }

    public void DamageEnemyPunch(Vector3 dir)
    {
        direction = dir * 70;
        StartCoroutine("Punch");
    }

    IEnumerator Punch()
    {
        agent.enabled = false;
        gameObject.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
        health--;

        yield return new WaitForSeconds(.25f);

        agent.enabled = true;
    }

    IEnumerator Kick()
    {
        agent.enabled = false;
        gameObject.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
        health -= 2;

        yield return new WaitForSeconds(.75f);
        if (Physics.Raycast(transform.position, -Vector3.up, 0.3f, enemyPlatform)) 
            agent.enabled = true;
    }

}
