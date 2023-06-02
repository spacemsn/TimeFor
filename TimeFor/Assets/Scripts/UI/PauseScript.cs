using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoCache
{
    public GloballSetting setting;

    [Header("Меню Респавна")]
    public Transform PausePanel;
    [SerializeField] public bool isOpenPanel = false;

    [SerializeField] private SettingsScript settingsMenu;

    private void Start()
    {
        setting = GetComponent<GloballSetting>();
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
            setting.notVisible(); setting.OpenMenu(PausePanel, isOpenPanel); OpenPanel();
        }
    }

    public void Exit()
    {
        SceneLoad.SwitchScene("Menu");
        setting.OpenMenu(PausePanel, isOpenPanel); OpenPanel();
        Time.timeScale = 1f;
    }

    public void Settings()
    {
        settingsMenu.OpenMenu();
        setting.OpenMenu(PausePanel, isOpenPanel); OpenPanel();
    }
}
