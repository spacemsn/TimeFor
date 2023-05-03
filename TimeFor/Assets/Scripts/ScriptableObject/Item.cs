using UnityEngine;

public enum ItemType { Default, Food, Potion, Weapon, Skill, Instrument }
public class Item : ScriptableObject
{
    [Header("Характеристики стандартного предмета")]
    [Header("Тип объекта")]
    public ItemType type;

    [Header("Название предмета")]
    public string name;

    [Header("Префаб предмета")]
    public GameObject itemPrefab;

    [Header("Максимальное кол-во в инвентаре")]
    public int maxAmount;

    [Header("Описание предмета")]
    public string aboutItem;

    [Header("Иконка предмета")]
    public Sprite icon;
}
