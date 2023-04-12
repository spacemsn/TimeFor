using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItem", menuName = "Inventory/Items/Food")]
public class FoodItem : Item
{
    public bool isConsumeable;

    public int changeHealth;
    public int changeHunger;
    public int changeThirst;
}
