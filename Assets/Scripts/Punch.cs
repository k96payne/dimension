using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Punch : MonoBehaviour
{
    LayerMask enemyPlatform = LayerMask.GetMask("EnemySurface");
    GameObject enemy;
    Vector3 direction;
    // Detects collision with enemy
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            PlayerController player = (PlayerController)GameObject.FindWithTag("Player").GetComponent(typeof(PlayerController));
            enemy = collider.gameObject;
            direction = player.transform.forward * 70;

            if (player.IsPunching())
            {
                StartCoroutine("PunchAI");
            }
        }
    }


    IEnumerator PunchAI()
    {
        enemy.GetComponent<NavMeshAgent>().enabled = false;
        enemy.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
        enemy.GetComponent<AIHealth>().damage(1);
        yield return new WaitForSeconds(.25f);
        if (enemy)
        {
           // if (Physics.Raycast(enemy.transform.position, -Vector3.up, 0.7f, enemyPlatform))
            //{
                enemy.GetComponent<NavMeshAgent>().enabled = true;
           // }
           // else
           // {
               // Destroy(enemy, 3);
           // }

        }
    }
}
