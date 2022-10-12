using UnityEngine;

public class ItemPrefab : MonoBehaviour
{
    public Item item;
    public Sprite icon;
    public int amount;

    [HideInInspector] public FoodItem food;
    [HideInInspector] public WeaponItem weapon;

    private void Start()
    {
        if(item.type == ItemType.Weapon) { weapon = (WeaponItem)item; }
        else if (item.type == ItemType.Food) { food = (FoodItem)item; }
    }

}
