using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    [Header("Время сохранения")]
    public string fileName;
    public string dateSave;

    [Header("Игрок")]
    public string playerName;

    [Header("Уровень")]
    public int levelId;

    [Header("Показатели")]
    public int levelPlayer;
    public int health;
    public float stamina;
    public float damageBase;
    public float damagePercent;

    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;
    public float debuff;

    [Header("Местонахождение")]
    public Vector3 position;

    public PlayerData(SaveData character)
    {
        fileName = playerName;
        dateSave = System.DateTime.Now.ToString();
        levelId = character.levelId;
        levelPlayer = character.levelPlayer;
        health = character.health;
        stamina = character.stamina;
        damageBase = character.damageBase;
        damagePercent = character.damagePercent;
        moveSpeed = character.moveSpeed;
        runSpeed = character.runSpeed;
        jumpForce = character.jumpForce;
        debuff = character.debuff;
        position = character.position;
    }

    public void LoadSave(SaveData character)
    {
        character.levelId = levelId;
        character.levelPlayer = levelPlayer;
        character.health = health;
        character.stamina = stamina;
        character.damageBase = damageBase;
        character.damagePercent = damagePercent;
        character.moveSpeed = moveSpeed;
        character.runSpeed = runSpeed;
        character.jumpForce = jumpForce;
        character.debuff = debuff;
        character.position = position;
    }
}
