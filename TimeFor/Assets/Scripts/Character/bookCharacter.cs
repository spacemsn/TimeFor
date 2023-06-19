using Cinemachine;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public int Id;
    public Item item;
    public foodItem foodItem;
    public ArtifactsObject artifact;
    public int amount;
    public bool isEmpty = true;

    public ItemType slotType;
    public ArtifactType artifactType;
    public bool isPut = false;

    public Color isPutColor;
    public Color isTakeColor;
}

public class bookCharacter : GloballSetting
{
    [Header("Книга")]
    [Header("Инвентарь")]
    [SerializeField] public Transform inventoryPage;
    [SerializeField] public Transform inventoryPanel;
    [SerializeField] public List<Slot> inventarySlots = new List<Slot>();
    [SerializeField] public bool isOpenInventory;

    [Header("Карта")]
    [SerializeField] public Transform mapPanel;
    [SerializeField] public bool isOpenMap;

    [Header("Сохранение инвентаря")]
    public SaveData saveInventory;
    public GloballSetting globall;

    [Header("Персонаж")]
    [SerializeField] public Transform playerPanel;
    [SerializeField] public List<Slot> playerSlots = new List<Slot>();

    private void Awake()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            if (inventoryPanel.transform.GetChild(i).GetComponent<Slot>() != null)
            {
                inventarySlots.Add(inventoryPanel.transform.GetChild(i).GetComponent<Slot>()); inventarySlots[i].GetComponent<Slot>().Id = i;
            }
        }

        for (int i = 0; i < saveInventory.inventorySlot.Count; i++)
        {
            Slot slot = inventoryPanel.transform.GetChild(i).GetComponent<Slot>();
            slot.Id = saveInventory.inventorySlot[i].Id;
            slot.item = saveInventory.inventorySlot[i].item;
            slot.amount = saveInventory.inventorySlot[i].amount;
            slot.isEmpty = saveInventory.inventorySlot[i].isEmpty;

            if (!slot.isEmpty)
            {
                slot.SetIcon(slot.item.icon);
                slot.itemAmount.text = slot.amount.ToString();
            }
        }

        for (int i = 0; i < playerPanel.transform.childCount; i++)
        {
            if (playerPanel.transform.GetChild(i).GetComponent<Slot>() != null)
            {
                playerSlots.Add(playerPanel.transform.GetChild(i).GetComponent<Slot>()); playerSlots[i].GetComponent<Slot>().Id = i;
            }
        }

        for (int i = 0; i < saveInventory.playerSlots.Count; i++)
        {
            Slot slot = playerPanel.transform.GetChild(i).GetComponent<Slot>();
            slot.Id = saveInventory.playerSlots[i].Id;
            slot.item = saveInventory.playerSlots[i].item;
            if (saveInventory.playerSlots[i].foodItem != null) { slot.foodItem = saveInventory.playerSlots[i].foodItem; }
            else if (saveInventory.playerSlots[i].artifact != null) { slot.artifact = saveInventory.playerSlots[i].artifact; }
            slot.amount = saveInventory.playerSlots[i].amount;
            slot.isEmpty = saveInventory.playerSlots[i].isEmpty;
            slot.slotType = saveInventory.playerSlots[i].slotType;
            slot.artifactType = saveInventory.playerSlots[i].artifactType;
            slot.isPut = saveInventory.playerSlots[i].isPut;

            slot.isPutColor = saveInventory.playerSlots[i].isPutColor;
            slot.isTakeColor = saveInventory.playerSlots[i].isTakeColor;

            if (!slot.isEmpty)
            {
                slot.SetIcon(slot.item.icon);
                slot.itemAmount.text = slot.amount.ToString();
            }

            if (slot.isPut)
            {
                slot.SetBuffIcon();
            }
        }
    }

    public void AddItem(Item _item, int _amount)
    {
        foreach (Slot slot in inventarySlots)
        {
            if (slot.item == _item && slot.amount + _amount <= _item.maxAmount)
            {
                slot.amount += _amount;
                slot.itemAmount.text = slot.amount.ToString();
                return;
            }
        }
        foreach (Slot slot in inventarySlots)
        {
            if (slot.isEmpty == true)
            {
                slot.item = _item;
                slot.amount = _amount;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemAmount.text = _amount.ToString();
                return;
            }
        }
    }

    public void OpenInventory()
    {
        isOpenInventory = !isOpenInventory;
    }

    public void OpenMap()
    {
        isOpenMap = !isOpenMap;
    }

    public void SaveInventory()
    {
        for (int i = 0; i < saveInventory.inventorySlot.Count; i++)
        {
            Slot slot = inventoryPanel.transform.GetChild(i).GetComponent<Slot>();
            slot.Id = i;
            saveInventory.inventorySlot[i].Id = slot.Id;
            saveInventory.inventorySlot[i].item = slot.item;
            saveInventory.inventorySlot[i].amount = slot.amount;
            saveInventory.inventorySlot[i].isEmpty = slot.isEmpty;
        }
    }

    public void SavePlayerArtifact()
    {
        for (int i = 0; i < saveInventory.playerSlots.Count; i++)
        {
            Slot slot = playerPanel.transform.GetChild(i).GetComponent<Slot>();
            slot.Id = i;
            saveInventory.playerSlots[i].Id = slot.Id;
            saveInventory.playerSlots[i].item = slot.item;
            if(slot.foodItem != null) { saveInventory.playerSlots[i].foodItem = slot.foodItem; }
            if (slot.artifact != null) { saveInventory.playerSlots[i].artifact = slot.artifact; }
            saveInventory.playerSlots[i].amount = slot.amount;
            saveInventory.playerSlots[i].isEmpty = slot.isEmpty;
            saveInventory.playerSlots[i].slotType = slot.slotType;
            saveInventory.playerSlots[i].artifactType = slot.artifactType;
            saveInventory.playerSlots[i].isPut = slot.isPut;

            saveInventory.playerSlots[i].isPutColor = slot.isPutColor;
            saveInventory.playerSlots[i].isTakeColor = slot.isTakeColor;
        }
    }
}
