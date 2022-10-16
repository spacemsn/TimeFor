using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoCache
{
    [Header("Меню Респавна")]
    public Transform PausePanel;
    [SerializeField] public bool isOpenPanel = false;

    public override void OnTick()
    {
        OpenMenu();
    }

    void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpenPanel == false)
            {
                PausePanel.gameObject.SetActive(true);
                isOpenPanel = true;
                // Видимость курсора
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
            else if (isOpenPanel == true)
            {
                PausePanel.gameObject.SetActive(false);
                isOpenPanel = false;
                // Видимость курсора
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
            }
        }
    }
    
    public void Continue()
    {
        if (isOpenPanel)
        {
            PausePanel.gameObject.SetActive(false);
            isOpenPanel = false;
            // Видимость курсора
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    public void Exit()
    {
        //SceneManager.LoadScene(0);
        SceneLoad.SwitchScene("Menu");
        Time.timeScale = 1f;
    }
}
