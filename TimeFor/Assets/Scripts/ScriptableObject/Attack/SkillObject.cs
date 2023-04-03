using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Object/ItemObject/Attack")]
public class SkillObject : ItemObject
{
    [Header("Характеристики скилла")]
    [Header("Потребление маны")]
    public int consumption;
    [Header("Урон")]
    public float damage;
    [Header("Откат способности")]
    public float attackRollback;
    [Header("Скорость")]
    public float speed;
}
