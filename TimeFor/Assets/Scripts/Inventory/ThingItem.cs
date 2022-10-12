using UnityEngine;

[CreateAssetMenu(fileName = "ThingItem", menuName = "Inventory/Items/Thing")]
public class ThingItem : Item
{
    [SerializeField] string aboutThing;
}
