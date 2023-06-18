using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "Object/ItemObject/Enemies")]
public class EnemyObject : ItemObject
{
    [Header("Характеристики врага")]
    [Header("Здоровье врага")]
    [SerializeField] private float hp;
    [SerializeField] private float baseHP;

    [Header("Урон врага")]
    [SerializeField] private int enemyDamage;
    [SerializeField] private int baseDamage;

    [Header("Угол обнуражения врага")]
    [SerializeField] private float viewAngle;

    [Header("Расстояние обнаружения врага")]
    [SerializeField] private float viewDistance;

    public void SetDamage(EnemyDamage enemy)
    {
        MainMenu.onNewGame += NewGame;

        enemy.hp = hp;
        enemy.enemyDamage = enemyDamage;

        enemy.healthBar.value = hp;
        enemy.healthBar.maxValue = hp;
    }

    public void SetBehavior(EnemyBehavior enemy)
    {
        enemy.viewAngle = viewAngle;
        enemy.viewDistance = viewDistance;
    }

    public void LevelUp(EnemyDamage enemy)
    {
        hp = enemy.hp;
    }

    public void NewGame()
    {
        hp = baseHP;
        enemyDamage = baseDamage;
    }
}
