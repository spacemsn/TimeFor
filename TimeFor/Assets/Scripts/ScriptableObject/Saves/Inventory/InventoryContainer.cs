using UnityEngine;

//[System.Serializable]
//public class InventorySlot
//{
//    public int Id;
//    public Item item;
//    public int amount;
//    public bool isEmpty = true;

//    public InventorySlot(int id, Item _item, int _amount)
//    {
//        Id = id;
//        item = _item;
//        amount = _amount;
//        isEmpty = true;
//    }
//}

public class InventoryContainer : MonoBehaviour
{
    public SaveData inventory;
    public SaveData defaultInventory;
    public Transform inventoryPanel;

    private void Start()
    {
        // Load saved inventory data
        inventory = Resources.Load<SaveData>("Character/Save");
        defaultInventory = Resources.Load<SaveData>("Inventory/Default");

        // Initialize inventory slots
        for (int i = 0; i < inventory.inventorySlot.Count; i++)
        {
            Slot slot = inventoryPanel.transform.GetChild(i).GetComponent<Slot>();
            slot.Id = inventory.inventorySlot[i].Id;
            slot.item = inventory.inventorySlot[i].item;
            slot.amount = inventory.inventorySlot[i].amount;
            slot.isEmpty = inventory.inventorySlot[i].isEmpty;

            if (!slot.isEmpty)
            {
                slot.SetIcon(slot.item.icon);
                slot.itemAmount.text = slot.amount.ToString();
            }
        }
    }

    public void SaveInventory()
    {
        // Save inventory data
        for (int i = 0; i < inventory.inventorySlot.Count; i++)
        {
            Slot slot = inventoryPanel.transform.GetChild(i).GetComponent<Slot>();
            inventory.inventorySlot[i].Id = slot.Id;
            inventory.inventorySlot[i].item = slot.item;
            inventory.inventorySlot[i].amount = slot.amount;
            inventory.inventorySlot[i].isEmpty = slot.isEmpty;
        }
    }

    public void SetDefaunt()
    {
        // Save inventory data
        for (int i = 0; i < defaultInventory.inventorySlot.Count; i++)
        {
            Slot slot = inventoryPanel.transform.GetChild(i).GetComponent<Slot>();
            slot.Id = defaultInventory.inventorySlot[i].Id;
            slot.item = defaultInventory.inventorySlot[i].item;
            slot.amount = defaultInventory.inventorySlot[i].amount;
            slot.isEmpty = defaultInventory.inventorySlot[i].isEmpty;
        }
    }
}