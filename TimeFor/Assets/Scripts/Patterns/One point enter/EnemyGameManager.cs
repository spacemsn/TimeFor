using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EnemyGameManager : MonoBehaviour
{
    [Header("������� ����������")]
    public PlayerEntryPoint player;
    public UIEntryPoint ui;

    [Header("����� �� �����")]
    public List<GameObject> Enemies;

    private void Start()
    {
        player = GetComponent<PlayerEntryPoint>();
        ui = GetComponent<UIEntryPoint>();

        SpawnContoller.onPlayerSceneLoaded += GetEnemyComponent;
        indicatorCharacter.onLevelUp += HealthUpEnemy;
    }

    private void OnDisable()
    {
        SpawnContoller.onPlayerSceneLoaded -= GetEnemyComponent;
        indicatorCharacter.onLevelUp -= HealthUpEnemy;
    }

    public void GetEnemyComponent()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();

        if(Enemies.Count > 0)
        {
            foreach(GameObject enemy in Enemies)
            {
                enemy.GetComponent<EnemyDamage>().player = player.currentPlayer.transform;
                enemy.GetComponent<EnemyDamage>().camera = player.currentCamera.transform;

                enemy.GetComponent<EnemyBehavior>().player = player.currentPlayer.transform;

                if(enemy.GetComponent<EnemyBehavior>().enemyLevel == IMoveBehavior.EnemyLevel.Враждебный)
                {
                    enemy.GetComponent<EnemyBehavior>().experience = 100;
                }
                else if (enemy.GetComponent<EnemyBehavior>().enemyLevel == IMoveBehavior.EnemyLevel.Опасный)
                {
                    enemy.GetComponent<EnemyBehavior>().experience = 500;
                }
                else if (enemy.GetComponent<EnemyBehavior>().enemyLevel == IMoveBehavior.EnemyLevel.Опасный)
                {
                    enemy.GetComponent<EnemyBehavior>().experience = 1000;
                }
            }
        }
    }

    public void HealthUpEnemy()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();

        if (Enemies.Count > 0)
        {
            foreach (GameObject enemy in Enemies)
            {
                if (enemy.GetComponent<EnemyBehavior>().enemyLevel == IMoveBehavior.EnemyLevel.Враждебный)
                {
                    enemy.GetComponent<EnemyDamage>().HealthUp();
                }
                else if (enemy.GetComponent<EnemyBehavior>().enemyLevel == IMoveBehavior.EnemyLevel.Опасный)
                {
                    enemy.GetComponent<EnemyDamage>().HealthUp();
                }
                else if (enemy.GetComponent<EnemyBehavior>().enemyLevel == IMoveBehavior.EnemyLevel.Демонический)
                {
                    enemy.GetComponent<EnemyDamage>().HealthUp();
                }
            }
        }
    }


}
