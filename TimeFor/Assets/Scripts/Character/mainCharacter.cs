using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float health;
    public float stamina;
    public float damageBase;
    public float damagePercent;

    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;
    public float debuff;

    [Header("Местонахождение")]
    public Vector3 position;

    [Header("Поворот")]
    public Quaternion rotation;

    [Header("Инвентарь")]
    public List<InventorySlot> slots = new List<InventorySlot>();

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
        position = character.currentPosition;

        slots = new List<InventorySlot>(character.slots);
    }

    public void LoadSave(SaveData character)
    {
        character.playerName = fileName;
        character.dateSave = dateSave;
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
        character.currentPosition = position;
    }
}

public class mainCharacter : MonoCache
{
    [Header("EntryPoint")]
    public PlayerEntryPoint playerEntry;
    public UIEntryPoint uIEntry;

    [Header("Игрок")]
    public string playerName;

    [Header("Компоненты игрока")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public Camera camera;

    [Header("Сохранение")]
    public SaveData saveData;

    [Header("Компоненты")]
    public attackCharacter attack;
    public indicatorCharacter indicators;
    public moveCharacter movement;
    public bookCharacter book;
    public DialogManager dialogManager;

    [Header("Последнее местоположение")]
    public Vector3 position;

    [Header("Поворот")]
    public Quaternion rotation;

    [Header("Характеристики персонажа")]
    public int levelId;
    public float damageBase;
    public float damagePercent;

    private void Start()
    {
        #region Components

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        movement = this.GetComponent<moveCharacter>();
        attack = this.GetComponent<attackCharacter>();
        indicators = this.GetComponent<indicatorCharacter>();
        dialogManager = this.GetComponent<DialogManager>();

        #endregion
    }

    public void GetUI(PlayerEntryPoint player, UIEntryPoint uI)
    {
        this.playerEntry = player;
        this.uIEntry = uI;

        book = player.book;
    }

    public int GetSceneIndex()
    {
        levelId = SceneManager.GetActiveScene().buildIndex;
        return levelId;
    }
}
