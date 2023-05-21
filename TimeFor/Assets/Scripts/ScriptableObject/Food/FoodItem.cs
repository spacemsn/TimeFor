using UnityEngine;

[CreateAssetMenu(fileName = "FoodItem", menuName = "Inventory/Items/Food")]
public class foodItem : Item
{
    [Header("Характеристики еды")]
    [Header("Съедобный предмет")]
    public bool isConsumeable;

    public int changeHealth;
    public int changeHunger;
    public int changeThirst;
}
