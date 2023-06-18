using System;
using Unity;
using UnityEngine;
using System.Collections;

public enum ArtifactType { Defaunt, Ring, Amulet, Headdress, }
[CreateAssetMenu(fileName = "Artifact", menuName = "Artifacts")]

public class ArtifactsObject : Item
{
    [Header("Тип артифакта")]
    public ArtifactType artifact;

    [Header("Типы увеличения характеристик")]
    [Header("Бонус колец")]
    public float healthIncrease;
    public float staminaIncrease;
    [Header("Бонус амулета")]
    public float damageIncrease;
    [Header("Бонус шляпы")]
    public float damagePercentIncrease;
}