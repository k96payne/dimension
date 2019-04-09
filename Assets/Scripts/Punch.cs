using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    // Detects collision with enemy
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            PlayerController player = (PlayerController)GameObject.FindWithTag("Player").GetComponent(typeof(PlayerController));
            EnemyController enemy = (EnemyController)collider.gameObject.GetComponent(typeof(EnemyController));

            if (player.IsPunching())
            {
                enemy.DamageEnemyPunch(GameObject.FindWithTag("Player").transform.forward);
            }
        }
    }
}
