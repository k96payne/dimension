using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public int health = 3;

    public void damage(int d)
    {
        health = health - d;

        if (health <= 0)
        {
            Destroy(gameObject, 0);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Death")
        {
            print("ENEMY DEATH BY FALL");
            if(gameObject)
            {
                Destroy(gameObject, 0);
            }
        }
    }

}