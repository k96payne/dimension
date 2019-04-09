using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager UI;
    private GameObject[] enemies;
    private GameObject player;
    private bool gamePaused = false;
    private GameObject menuPanel;
    public bool lockCursor = false;


    void Start()
    {
        Cursor.visible = lockCursor;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        menuPanel = GameObject.FindGameObjectWithTag("Menu");
    }

    void Update()
    {
        Cursor.visible = gamePaused;
    }

    public void TogglePauseMenu()
    {
        //print("TEST");
        gamePaused = !gamePaused;
        ToggleEnemyAgents();
        TogglePlayerMovement();
        TogglePausePanel();
    }

    private void ToggleCursor()
    {
        lockCursor = !lockCursor;
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        //Cursor.visible = !lockCursor;
    }

    private void TogglePausePanel()
    {
        menuPanel.gameObject.SetActive(gamePaused);
        menuPanel.GetComponent<CanvasGroup>().alpha = gamePaused ? 1.0f : 0.0f;
    }

    private void ToggleEnemyAgents()
    {
        foreach (GameObject enemy in enemies)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.agent.enabled = !enemyController.agent.enabled;
        }
    }

    private void TogglePlayerMovement()
    {
        player.GetComponent<PlayerController>().ToggleMovement();
    }

}
