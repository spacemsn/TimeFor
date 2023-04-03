using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoCache
{
    [Header("Инвентарь")]
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private List<Slot> slots = new List<Slot>();
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] public bool isOpenPanel;

    [SerializeField] private CharacterStatus status;
    [SerializeField] private GloballSetting globallSetting;
 
    private void Start()
    {
        globallSetting = GetComponent<GloballSetting>();
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>();
        freeLook = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineFreeLook>();

        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<Slot>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<Slot>());
            }
        }
    }

    public void AddItem(Item _item, int _amount)
    {
        foreach (Slot slot in slots)
        {
            if (slot.item == _item)
            {
                if (slot.amount + _amount <= _item.maxAmount)
                {
                    slot.amount += _amount;
                    slot.itemAmount.text = slot.amount.ToString();
                    return;
                }
                break;
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
                break;
            }
        }
    }

    public void AddItemFood(FoodItem _item, int _amount)
    {
        foreach (Slot slot in slots)
        {
            if (slot.foodItem == _item)
            {
                if (slot.amount + _amount <= _item.maxAmount)
                {
                    slot.amount += _amount;
                    slot.itemAmount.text = slot.amount.ToString();
                    return;
                }
                break;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.isEmpty == true)
            {
                slot.foodItem = _item;
                slot.amount = _amount;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemAmount.text = _amount.ToString();
                break;
            }
        }
    }

    public void OpenInventory()
    {
        if (isOpenPanel == true)
        {
            inventoryPanel.gameObject.SetActive(false);
            isOpenPanel = false;
            freeLook.m_XAxis.m_InputAxisName = "Mouse X";
            freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
            status.charMenegment = true;
            globallSetting.notVisible();
        }
        else if (isOpenPanel == false)
        {
            inventoryPanel.gameObject.SetActive(true);
            isOpenPanel = true;
            freeLook.m_XAxis.m_InputAxisName = "";
            freeLook.m_XAxis.m_InputAxisValue = 0;
            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisValue = 0;
            status.charMenegment = false;
            globallSetting.Visible();
        }
    }
}
