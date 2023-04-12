using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [Header("Хранимые данные")]
    public int level;
    public int health;
    public float stamina;
    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;
    public float debuff;
    public Vector3 position;

    public PlayerData(CharacterStatus status)
    {
        level = status.levelId;
        health = status.health;
        stamina = status.stamina;
        moveSpeed = status.moveSpeed;
        runSpeed = status.runSpeed;
        jumpForce = status.jumpForce;
        debuff = status.debuff;
        position = status.position;
    }

}
