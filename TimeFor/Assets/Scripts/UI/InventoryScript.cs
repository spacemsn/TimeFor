using Cinemachine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryScript : MonoCache
{
    [Header("Инвентарь")]
    [SerializeField] public GameObject inventoryPanel;
    [SerializeField] public List<Slot> slots = new List<Slot>();
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] public bool isOpenPanel = true;

    [SerializeField] private CharacterStatus status;
    [SerializeField] private GloballSetting globallSetting;
 
    private void Start()
    {
        globallSetting = GameObject.Find("Global Settings").GetComponent<GloballSetting>();
    }

    public void SetComponent(GameObject inventoryPanel, CharacterStatus status, CinemachineFreeLook freeLook)
    {
        this.inventoryPanel = inventoryPanel;
        this.status = status;
        this.freeLook = freeLook;

        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            if (inventoryPanel.transform.GetChild(i).GetComponent<Slot>() != null)
            {
                slots.Add(inventoryPanel.transform.GetChild(i).GetComponent<Slot>()); slots[i].GetComponent<Slot>().Id = i;
            }
        }

        OpenInventory();
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
