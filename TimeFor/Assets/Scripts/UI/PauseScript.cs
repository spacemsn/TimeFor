using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoCache
{
    [Header("Меню Респавна")]
    public GameObject PausePanel;
    [SerializeField] public bool isOpenPanel = false;

    [SerializeField] private SettingsScript settingsMenu;

    private void Start()
    {
        settingsMenu = GetComponent<SettingsScript>();
    }

    public void SetComponent(GameObject PausePanel)
    {
        this.PausePanel = PausePanel;
        OpenMenu();
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
