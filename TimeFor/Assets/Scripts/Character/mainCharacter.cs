using System;
using UnityEngine;

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
    [Header("Игрок")]
    public string playerName;

    [Header("Компоненты игрока")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public Camera camera;

    [Header("Сохранение")]
    public SaveData saveData;

    [Header("Инвентарь")]
    public bookCharacter inventory;

    [Header("Компоненты")]
    public attackCharacter attack;
    public indicatorCharacter indicators;
    public moveCharacter movement;
    public interactionCharacter interaction;
    public bookCharacter book;
    public DialogManager dialogManager;

    [Header("Последнее местоположение")]
    public Vector3 position;

    [Header("Характеристики персонажа")]
    public int levelId;
    public float damageBase;
    public float damagePercent;

    private void Start()
    {
        #region Components

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        inventory = GetComponent<bookCharacter>();
        movement = this.GetComponent<moveCharacter>();
        attack = this.GetComponent<attackCharacter>();
        indicators = this.GetComponent<indicatorCharacter>();
        interaction = this.GetComponent<interactionCharacter>();
        book = this.GetComponent<bookCharacter>();
        dialogManager = this.GetComponent<DialogManager>();

        #endregion
    }
}
