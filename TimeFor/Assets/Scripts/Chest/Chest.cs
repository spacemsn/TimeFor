using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public static Action<int> onOpenChest;

    [Header("’арактеристики стандартного сундука")]
    public ChestObject chest;

    public Transform pointItem;
    public float forceItem;
    public float radius;
    public LayerMask maskPlayer;

    public Collider[] Player;

    public Button buttonPrefab;
    public Transform buttonParent;
    public Button currentButton;

    public GameObject currentItem;

    private void Update()
    {
        Player = Physics.OverlapSphere(pointItem.position, radius, maskPlayer);
        if (Player.Length > 0 && currentButton == null)
        {
            buttonParent = GameObject.FindGameObjectWithTag("ButtonPanel").transform;
            currentButton = Instantiate(buttonPrefab, buttonParent);
            currentButton.GetComponent<SelectObjectButton>().GetComponentChest(this, Player[0].gameObject);
            currentButton.transform.GetChild(0).GetComponent<Text>().text = "(F)    открыть " + chest.name;

        }
        else if (Player.Length == 0 && currentButton != null)
        {
            Destroy(currentButton.gameObject);
        }
    }

    public void OpenChest()
    {
        for (int i = 0; i < chest.item.Count; i++)
        {
            var item = chest.item[i];
            for(int j = 0; j < chest.item[i].amount; j++)
{
                currentItem = Instantiate(item.prefab.itemPrefab, pointItem.position, pointItem.rotation);
                currentItem.GetComponent<Rigidbody>().AddForce(-Player[0].transform.forward * forceItem, ForceMode.Impulse);
            }
        }
    }

    public void OnDestroy()
    {
        if (currentButton != null)
        {
            Destroy(currentButton.gameObject);
            onOpenChest.Invoke(100);
        }
    }
}
