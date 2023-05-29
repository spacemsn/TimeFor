using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public int Id;
    public Item item;
    public int amount;
    public bool isEmpty = true;

    public InventorySlot(int id, Item _item, int _amount)
    {
        Id = id;
        item = _item;
        amount = _amount;
        isEmpty = true;
    }
}

public class bookCharacter : MonoCache
{
    [Header("Книга")]
    [Header("Инвентарь")]
    [SerializeField] public GameObject inventoryPage;
    [SerializeField] public GameObject inventoryPanel;
    [SerializeField] public List<Slot> slots = new List<Slot>();
    [SerializeField] public bool isOpenInventory;

    [Header("Карта")]
    [SerializeField] private GameObject mapPanel;
    [SerializeField] public bool isOpenMap;

    [Header("Компоненты")]
    [SerializeField] private CinemachineFreeLook freeLook;

    [Header("Доп. скрипты")]
    public EntryPoint EntryPoint;

    [Header("Сохранение инвентаря")]
    public SaveData saveInventory;
    public SaveData defaultInventory;

    private void Awake()
    {
        EntryPoint = GetComponentInParent<EntryPoint>();
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            if (inventoryPanel.transform.GetChild(i).GetComponent<Slot>() != null)
            {
                slots.Add(inventoryPanel.transform.GetChild(i).GetComponent<Slot>()); slots[i].GetComponent<Slot>().Id = i;
            }
        }

        // Initialize inventory slots
        for (int i = 0; i < saveInventory.slots.Count; i++)
        {
            Slot slot = inventoryPanel.transform.GetChild(i).GetComponent<Slot>();
            slot.Id = saveInventory.slots[i].Id;
            slot.item = saveInventory.slots[i].item;
            slot.amount = saveInventory.slots[i].amount;
            slot.isEmpty = saveInventory.slots[i].isEmpty;

            if (!slot.isEmpty)
            {
                slot.SetIcon(slot.item.icon);
                slot.itemAmount.text = slot.amount.ToString();
            }
        }
    }

    public void AddItem(Item _item, int _amount)
    {
        foreach (Slot slot in slots)
        {
            if (slot.item == _item && slot.amount + _amount <= _item.maxAmount)
            {
                slot.amount += _amount;
                slot.itemAmount.text = slot.amount.ToString();
                return;
            }
        }
        foreach (Slot slot in slots)
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
        if (isOpenInventory == true)
        {
            inventoryPage.gameObject.SetActive(false);
            isOpenInventory = false;
            freeLook.m_XAxis.m_InputAxisName = "Mouse X";
            freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
            EntryPoint.player.movement.isManagement = true;
            EntryPoint.globallSetting.globall.notVisible();
        }
        else if (isOpenInventory == false)
        {
            inventoryPage.gameObject.SetActive(true);
            isOpenInventory = true;
            freeLook.m_XAxis.m_InputAxisName = "";
            freeLook.m_XAxis.m_InputAxisValue = 0;
            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisValue = 0;
            EntryPoint.player.movement.isManagement = false;
            EntryPoint.globallSetting.globall.Visible();
        }
    }

    public void OpenMap()
    {
        if (isOpenMap == true)
        {
            mapPanel.gameObject.SetActive(false);
            isOpenMap = false;
            freeLook.m_XAxis.m_InputAxisName = "Mouse X";
            freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
            EntryPoint.player.movement.isManagement = true;
        }
        else if (isOpenMap == false)
        {
            mapPanel.gameObject.SetActive(true);
            isOpenMap = true;
            freeLook.m_XAxis.m_InputAxisName = "";
            freeLook.m_XAxis.m_InputAxisValue = 0;
            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisValue = 0;
            EntryPoint.player.movement.isManagement = false;
        }
    }

    public void SaveInventory()
    {
        // Save inventory data
        for (int i = 0; i < saveInventory.slots.Count; i++)
        {
            Slot slot = inventoryPanel.transform.GetChild(i).GetComponent<Slot>();
            saveInventory.slots[i].Id = slot.Id;
            saveInventory.slots[i].item = slot.item;
            saveInventory.slots[i].amount = slot.amount;
            saveInventory.slots[i].isEmpty = slot.isEmpty;
        }
    }

    public void SetDefaunt()
    {
        // Save inventory data
        for (int i = 0; i < defaultInventory.slots.Count; i++)
        {
            Slot slot = inventoryPanel.transform.GetChild(i).GetComponent<Slot>();
            slot.Id = defaultInventory.slots[i].Id;
            slot.item = defaultInventory.slots[i].item;
            slot.amount = defaultInventory.slots[i].amount;
            slot.isEmpty = defaultInventory.slots[i].isEmpty;
        }
    }
}
