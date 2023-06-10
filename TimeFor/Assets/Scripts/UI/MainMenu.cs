using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Сохранение персонажа")]
    [SerializeField] private SaveData currentSave;

    [Header("Настройки")]
    [SerializeField] private SettingsScript settings;

    EntryPoint entryPoint;

    private void Start()
    {
        entryPoint = GameObject.Find("EntryPoint").GetComponent<EntryPoint>();
        currentSave = entryPoint.player.saveData;
    }

    public void NewGame()
    {
        SceneLoad.SwitchIndexScene(SceneManager.GetActiveScene().buildIndex + 1);
        entryPoint.globallSetting.globall.notVisible();
        Time.timeScale = 1f;
    }

    public void Continuo()
    {
        SceneLoad.SwitchIndexScene(currentSave.savedData[currentSave.savedData.Count - 1].levelId);
        entryPoint.globallSetting.globall.notVisible();
        Time.timeScale = 1f;
    }

    public void ExiteGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        settings.OpenMenu();
    }
}
