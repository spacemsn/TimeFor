using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ChestItemAmount
{
    public int amount;
    public Item prefab;
}

public enum ChestType { Base, Big, Bigger, theBiggest }
[CreateAssetMenu(fileName = "Chest", menuName = "Chest")]
public class ChestObject : ScriptableObject
{
    [Header("Характеристики стандартного сундука")]
    [Header("Тип объекта")]
    public ChestType type;

    [Header("Название предмета")]
    public string name;

    [Header("Префаб предмета")]
    public GameObject chestPrefab;

    [Header("Список выпающих предметов")]
    public List<ChestItemAmount> item;

    [Header("Максимальное кол-во предмета в сундуке")]
    public int maxAmount;

    [Header("Описание предмета")]
    public string aboutChest;

    [Header("Иконка предмета")]
    public Sprite icon;

}
