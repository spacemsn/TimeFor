using UnityEngine;
using UnityEngine.UI;

public enum ItemType { Default, Food, Potion, Weapon, Skill, Artifact, Instrument }
public class Item : ScriptableObject
{
    [Header("�������������� ������������ ��������")]
    [Header("��� �������")]
    public ItemType type;

    [Header("�������� ��������")]
    public string name;

    [Header("������ ��������")]
    public GameObject itemPrefab;

    [Header("������������ ���-�� � ���������")]
    public int maxAmount;

    [Header("�������� ��������")]
    public string aboutItem;

    [Header("������ ��������")]
    public Sprite icon;
}
