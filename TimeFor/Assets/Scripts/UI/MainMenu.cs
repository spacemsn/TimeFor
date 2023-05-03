using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Сохранение персонажа")]
    [SerializeField] private SaveData currentSave;
    [SerializeField] private SaveData defaultSave;
    [SerializeField] private GameObject player;

    [Header("Настройки")]
    [SerializeField] private SettingsScript settings;

    private void Start()
    {
        currentSave = Resources.Load<SaveData>("Character/Save");
        defaultSave = Resources.Load<SaveData>("Character/Default");
        player = Resources.Load<GameObject>("Prefabs/Player/Character");
    }

    public void NewGame()
    {
        //currentSave.savedPlayers.Clear();
        //currentSave.SetSave(player.GetComponent<CharacterStatus>());
        SceneLoad.SwitchScene("LVL1");
        Time.timeScale = 1f;
    }

    public void Continuo()
    {
        //if(currentSave.savedPlayers.Count <= 0)
        //{
        //    Debug.Log("У вас нет сохранений, начните новую игру!");
        //}
        //else
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + currentSave.levelId);
        //    Time.timeScale = 1f;
        //}

        SceneLoad.SwitchScene("LVL2");
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
