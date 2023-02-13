using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public int mana;
    public float stamina;
    public Vector3 position;

    public PlayerData(CharacterStatus status)
    {
        level = status._levelId;
        health = status._health;
        mana = status._mana;
        stamina = status._stamina;
        position = status._position;
    }

}
