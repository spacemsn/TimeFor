using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItem", menuName = "Inventory/Items/Weapon")]
public class weaponItem : Item
{
    [Header("Характеристики оружия")]
    [Header("Урон")]
    public float damage;
}
