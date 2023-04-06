using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    [Header("Сохранение")]
    public CharacterObject character;

    [Header("Хранимые данные")]
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

    public SaveData(CharacterStatus status)
    {
        levelId = status.levelId;
        health = status.health;
        stamina = status.stamina;
        moveSpeed = status.moveSpeed;
        runSpeed = status.runSpeed;
        jumpForce = status.jumpForce;
        debuff = status.debuff;
        position = status.position;
    }

    public void SavePlayer()
    {

    }

    public void LoadPlayer()
    {

    }
}
