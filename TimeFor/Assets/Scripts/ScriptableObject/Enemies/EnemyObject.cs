using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "Object/ItemObject/Enemies")]
public class EnemyObject : ItemObject
{
    [Header("Характеристики врага")]
    [Header("Здоровье врага")]
    public float hp;
    [Header("Урон врага")]
    public int enemyDamage;
    [Header("Угол обнуражения врага")]
    public float viewAngle;
    [Header("Расстояние обнаружения врага")]
    public float viewDistance;
}
