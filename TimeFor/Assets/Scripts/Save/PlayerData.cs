using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [Header("Хранимые данные")]
    public int level;
    public int health;
    public int mana;
    public float stamina;
    public Vector3 position;

    public PlayerData(CharacterStatus status)
    {
        level = status.levelId;
        health = status.health;
        mana = status.mana;
        stamina = status.stamina;
        position = status.position;
    }

}
