using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoCache
{
    [Header("Меню Респавна")]
    public Transform PausePanel;
    [SerializeField] public bool isOpenPanel = false;

    [SerializeField] private SettingsMenu settingsMenu;

    private void Start()
    {
        settingsMenu = GetComponent<SettingsMenu>();
    }

    public void OpenMenu()
    {
        if (isOpenPanel == false)
        {
            PausePanel.gameObject.SetActive(true);
            isOpenPanel = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else if (isOpenPanel == true)
        {
            PausePanel.gameObject.SetActive(false);
            isOpenPanel = false;
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            //Time.timeScale = 1f;
        }
    }
    
    public void Continue()
    {
        if (isOpenPanel)
        {
            PausePanel.gameObject.SetActive(false);
            isOpenPanel = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    public void Exit()
    {
        SceneLoad.SwitchScene("Menu");
        Time.timeScale = 1f;
    }

    public void Settings()
    {
        settingsMenu.OpenMenu();
        OpenMenu();
    }
}
