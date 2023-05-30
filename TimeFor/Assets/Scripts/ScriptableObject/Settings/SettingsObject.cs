using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Object/SettingsObject/Settings")]
public class SettingsObject : ScriptableObject
{
    [Header("Настройки игры")]
    [Header("Controls")]
    [Header("настройка мыши")]
    public float SensitivityY;
    public float SensitivityX;


    //[Header("Video")]


    //[Header("Audio")]


    //[Header("Language")]

}
