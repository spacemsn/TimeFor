using UnityEngine;

public enum ObjectType { Default, Potion, Attack, Instrument, Enemies }
public class ItemObject: ScriptableObject
{
    [Header("Характеристики стандартного предмета")]
    [Header("Тип объекта")]
    public ObjectType type;
    [Header("Название предмета")]
    public string name;
    [Header("Префаб предмета")]
    public GameObject objectPrefab;
    [Header("Максимальное кол-во в инвентаре")]
    public int maxAmount;
    [Header("Описание предмета")]
    public string aboutObject;
    [Header("Иконка предмета")]
    public Sprite icon;
}
