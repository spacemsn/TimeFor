using UnityEngine;
using UnityEngine.UI;

public enum ItemType { Default, Food, Potion, Weapon, Skill, Artifact, Element, }
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
    [TextArea(order = 500)]
    public string aboutItem;

    [Header("Иконка предмета")]
    public Sprite icon;
}
