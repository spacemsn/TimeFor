using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : GloballSetting
{
    [Header("Меню Респавна")]
    public Transform PausePanel;
    [SerializeField] public bool isOpenPanel = false;

    [SerializeField] private SettingsScript settingsMenu;
    public GloballSetting globall;

    private void Start()
    {
        settingsMenu = GetComponent<SettingsScript>();
    }

    public void OpenPanel()
    {
        isOpenPanel = !isOpenPanel;
    }
    
    public void Continue()
    {
        if (isOpenPanel)
        {
            OpenMenu(PausePanel, isOpenPanel); OpenPanel(); ResumeGame(); notVisible();
        }
    }

    public void Exit()
    {
        OpenMenu(PausePanel, isOpenPanel); 
        OpenPanel();
        ResumeGame();
        SceneLoad.SwitchScene("Menu");
    }

    public void Settings()
    {
        settingsMenu.OpenMenu();
        OpenMenu(PausePanel, isOpenPanel); OpenPanel();
    }
}
