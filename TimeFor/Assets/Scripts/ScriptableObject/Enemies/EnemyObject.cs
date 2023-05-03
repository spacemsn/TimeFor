using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "Object/ItemObject/Enemies")]
public class EnemyObject : ItemObject
{
    [Header("Характеристики врага")]
    [Header("Здоровье врага")]
    [SerializeField] private float hp;

    [Header("Урон врага")]
    [SerializeField] private int enemyDamage;

    [Header("Угол обнуражения врага")]
    [SerializeField] private float viewAngle;

    [Header("Расстояние обнаружения врага")]
    [SerializeField] private float viewDistance;

    public void SetDamage(EnemyDamage enemy)
    {
        enemy.hp = hp;
        enemy.enemyDamage = enemyDamage;
    }

    public void SetBehavior(EnemyBehavior enemy)
    {
        enemy.viewAngle = viewAngle;
        enemy.viewDistance = viewDistance;
    }
}
