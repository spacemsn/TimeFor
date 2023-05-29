using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EnemyGameManager : MonoBehaviour
{
    [Header("Игровые компоненты")]
    public PlayerEntryPoint player;
    public UIEntryPoint ui;

    [Header("Враги на сцене")]
    public List<GameObject> Enemies;

    private void Start()
    {
        player = GetComponent<PlayerEntryPoint>();
        ui = GetComponent<UIEntryPoint>();
    }

    public void GetEnemyComponent()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();

        if(Enemies.Count > 0)
        {
            foreach(GameObject enemy in Enemies)
            {
                enemy.GetComponent<EnemyDamage>().player = player.currentPlayer.transform;
                enemy.GetComponent<EnemyDamage>().camera = ui.camera.transform;

                enemy.GetComponent<EnemyBehavior>().player = player.currentPlayer.transform;
            }
        }
    }


}
