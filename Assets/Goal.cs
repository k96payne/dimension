using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject sceneManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "GameEndTrigger")
        {
            Cursor.lockState =CursorLockMode.None;
            Cursor.visible = true;
            sceneManager.GetComponent<LoadSceneOnClick>().LoadByIndex(5);
        }
    }
}
