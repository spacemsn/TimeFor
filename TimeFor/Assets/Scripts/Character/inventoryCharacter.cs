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

public class inventoryCharacter : MonoCache
{
    [Header("Инвентарь")]
    [SerializeField] private GameObject inventoryPage;
    [SerializeField] public GameObject inventoryPanel;
    [SerializeField] public List<Slot> slots = new List<Slot>();
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] public bool isOpenPanel;

    [Header("Сохранение инвентаря")]
    public SaveData saveInventory;
    public SaveData defaultInventory;

    [SerializeField] private moveCharacter move;
    [SerializeField] private GloballSetting globallSetting;
    private void Start()
    {
        globallSetting = GameObject.Find("Global Settings").GetComponent<GloballSetting>();
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<moveCharacter>();
        freeLook = GameObject.FindGameObjectWithTag("FreeLook").GetComponent<CinemachineFreeLook>();

        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            if (inventoryPanel.transform.GetChild(i).GetComponent<Slot>() != null)
            {
                slots.Add(inventoryPanel.transform.GetChild(i).GetComponent<Slot>()); slots[i].GetComponent<Slot>().Id = i;
            }
        }

        // Load saved inventory data
        saveInventory = Resources.Load<SaveData>("Character/Save");
        defaultInventory = Resources.Load<SaveData>("Inventory/Default");

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
        if (isOpenPanel == true)
        {
            inventoryPage.gameObject.SetActive(false);
            isOpenPanel = false;
            freeLook.m_XAxis.m_InputAxisName = "Mouse X";
            freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
            move.isManagement = true;
            globallSetting.notVisible();
        }
        else if (isOpenPanel == false)
        {
            inventoryPage.gameObject.SetActive(true);
            isOpenPanel = true;
            freeLook.m_XAxis.m_InputAxisName = "";
            freeLook.m_XAxis.m_InputAxisValue = 0;
            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisValue = 0;
            move.isManagement = false;
            globallSetting.Visible();
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
