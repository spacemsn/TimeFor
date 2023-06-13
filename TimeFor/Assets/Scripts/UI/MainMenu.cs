using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Сохранение персонажа")]
    [SerializeField] private SaveData currentSave;

    [Header("Настройки")]
    [SerializeField] private SettingsScript settings;

    [Header("Панель уведомлений")]
    public Transform warningPanel;
    public Text warningText;

    EntryPoint entryPoint;

    private void Start()
    {
        entryPoint = GameObject.Find("EntryPoint").GetComponent<EntryPoint>();
        currentSave = entryPoint.player.saveData;

        warningPanel.gameObject.SetActive(false);
    }

    public void NewGame()
    {
        if (currentSave.savedData.Count <= 0) // проверка на рабочие сохранения у игрока
        {
            SceneLoad.SwitchIndexScene(SceneManager.GetActiveScene().buildIndex + 1);
            entryPoint.globallSetting.globall.notVisible();
            Time.timeScale = 1f;

            currentSave.savedData.Clear();
        }
        else
        {
            string warningContext = "У вас уже есть сохранения, если вы нажмете новую игру, то ваши предыдущие сохранения удаляться.";

            warningPanel.gameObject.SetActive(true); warningText.text = warningContext;
        }
    }

    public void ClearAllSaves()
    {
        SceneLoad.SwitchIndexScene(SceneManager.GetActiveScene().buildIndex + 1);
        entryPoint.globallSetting.globall.notVisible();
        Time.timeScale = 1f;

        currentSave.savedData.Clear();
    }

    public void Continuo()
    {
        if (currentSave.savedData.Count > 0) // проверка на рабочие сохранения у игрока
        {
            SceneLoad.SwitchIndexScene(currentSave.savedData[currentSave.savedData.Count - 1].LevelScene);
            entryPoint.globallSetting.globall.notVisible();
            Time.timeScale = 1f;
        }
        else
        {
            string warningContext = "У вас нет сохранений, пожалуйста нажмите новую игру";

            warningPanel.gameObject.SetActive(true); warningText.text = warningContext;
        }
    }

    public void ExiteGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        settings.OpenMenu();
    }

    public void CloseWarningPanel()
    {
        warningText.text = "";
        warningPanel.gameObject.SetActive(false);
    }
}
