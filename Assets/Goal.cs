using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject sceneManager;

    private void OnTriggerEnter(Collider other)
    {
        sceneManager.GetComponent<LoadSceneOnClick>().LoadByIndex(5);
    }
}
