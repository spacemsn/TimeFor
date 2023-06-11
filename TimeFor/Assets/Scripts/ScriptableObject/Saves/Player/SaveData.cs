using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save player", menuName = "Save")]
public class SaveData : ItemObject
{
    [Header("Время сохранения")]
    public string dateSave;

    [Header("Игрок")]
    public string playerName;

    [Header("Уровень")]
    public int levelId;

    [Header("Показатели")]
    public int levelPlayer;
    public float health;
    public float stamina;
    public float damageBase;
    public float damagePercent;

    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;
    public float debuff;

    [Header("Местонахождение")]
    public Vector3 currentPosition;
    public Vector3 lastSavePosition;

    [Header("Поворот")]
    public Quaternion rotation;

    [Header("Инвентарь")]
    public List<InventorySlot> slots = new List<InventorySlot>();

    // Загрузить список сохраненных игроков
    PlayerData playerData;
    public List<PlayerData> savedData;

    public void SetSave(mainCharacter character, indicatorCharacter indicators, moveCharacter move)
    {
        character.GetSceneIndex();

        levelId = character.levelId;
        levelPlayer = indicators.level;
        health = indicators.Health;
        stamina = indicators.Stamina;
        damageBase = character.damageBase;
        damagePercent = character.damagePercent;
        moveSpeed = move.moveSpeed;
        runSpeed = move.runSpeed;
        jumpForce = move.jumpForce;
        debuff = move.debuff;
        currentPosition = move.gameObject.transform.position;
        rotation = move.gameObject.transform.rotation;

        character.book.SaveInventory();

        playerData = new PlayerData(this);
        savedData.Add(playerData);
    }

    public void LoadSave(mainCharacter character, indicatorCharacter indicators, moveCharacter move)
    {
        savedData[savedData.Count - 1].LoadSave(this);

        indicators.level = levelPlayer;
        indicators.Health = health;
        indicators.Stamina = stamina;
        character.damageBase = damageBase;
        character.damagePercent = damagePercent;
        move.moveSpeed = moveSpeed;
        move.runSpeed = runSpeed;
        move.jumpForce = jumpForce;
        move.debuff = debuff;
        move.gameObject.transform.position = currentPosition;
        move.gameObject.transform.rotation = rotation;

        character.book.SaveInventory();
    }
}
