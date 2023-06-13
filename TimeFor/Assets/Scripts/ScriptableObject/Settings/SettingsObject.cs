using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Settings", menuName = "Object/SettingsObject/Settings")]
public class SettingsObject : ScriptableObject
{
    [Header("Настройки игры")]

    [Header("Настройка мыши")]
    public float SensitivityY;
    public float SensitivityX;

    [Header("Настройка звука")]
    public float MusicValue;
    public float SoundValue;
    public bool isMute;


    //[Header("Video")]


    //[Header("Audio")]


    //[Header("Language")]

    [Header("Сохранение")]
    [SerializeField] private Controlls controlls;
    

    public void SetSave(SettingsScript setting)
    {
        SensitivityY = setting.SensitivityYSlider.value;
        SensitivityX = setting.SensitivityXSlider.value;

        MusicValue = setting.MusicValue.value;
        SoundValue = setting.SoundValue.value;
        isMute = setting.isMute;

        controlls = new Controlls(this);
    }

    public void LoadSave(SettingsScript setting)
    {
        controlls.LoadControlls(this);

        setting.SensitivityYSlider.value = SensitivityY;
        setting.SensitivityXSlider.value = SensitivityX;

        setting.MusicValue.value = MusicValue;
        setting.SoundValue.value = SoundValue;

        setting.isMute = isMute;

    }
}
