using UnityEngine;

public enum ItemType { Default, Food, Weapon, Instrument }
public class Item : ScriptableObject
{
    public ItemType type;
    public string Name;
    public GameObject itemPrefab;
    public int maxAmount;
    public string aboutItem;
    public Sprite icon;
}
