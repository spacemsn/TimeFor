using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemGameManager : MonoBehaviour
{
    [Header("Игровые компоненты")]
    public PlayerEntryPoint player;
    public UIEntryPoint ui;

    [Header("Враги на сцене")]
    public List<GameObject> Items;

    private void Start()
    {
        player = GetComponent<PlayerEntryPoint>();
        ui = GetComponent<UIEntryPoint>();
    }

    public void GetItemComponent()
    {
        Items = GameObject.FindGameObjectsWithTag("Selectable").ToList();

        if (Items.Count > 0)
        {
            foreach (GameObject enemy in Items)
            {
                enemy.GetComponent<EnemyDamage>().player = player.currentPlayer.transform;
                enemy.GetComponent<EnemyDamage>().camera = ui.camera.transform;

                enemy.GetComponent<EnemyBehavior>().player = player.currentPlayer.transform;
            }
        }
    }
}
