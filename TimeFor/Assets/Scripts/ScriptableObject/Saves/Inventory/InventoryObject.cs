using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory Object")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> slots = new List<InventorySlot>();
}