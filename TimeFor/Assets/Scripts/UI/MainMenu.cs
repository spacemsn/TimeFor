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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    public void Continuo()
    {
        SceneManager.LoadScene(currentSave.savedPlayers[currentSave.savedPlayers.Count - 1].levelId);
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
