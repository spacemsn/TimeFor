using UnityEngine;

public enum ObjectType { Default, Enemies, Players, NPC }
public class ItemObject : ScriptableObject
{
    [Header("Характеристики стандартного предмета")]
    [Header("Тип объекта")]
    public ObjectType type;

    [Header("Название предмета")]
    public string name;

    [Header("Префаб предмета")]
    public GameObject objectPrefab;

    [Header("Описание предмета")]
    public string aboutObject;

    [Header("Иконка предмета")]
    public Sprite icon;

}
