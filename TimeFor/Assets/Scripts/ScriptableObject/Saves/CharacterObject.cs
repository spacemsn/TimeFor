using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Object/ItemObject/Character")]
public class CharacterObject : ItemObject
{
    [Header("Игрок")]

    [Header("Уровень")]
    public int levelId;

    [Header("Показатели")]
    public int health;
    public float stamina;
    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;
    public float debuff;

    [Header("Местонахождение")]
    public Vector3 position;

}
