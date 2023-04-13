using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Сохранение персонажа")]
    [SerializeField] private CharacterObject save;

    [Header("Настройки")]
    [SerializeField] private SettingsScript settings;

    public void NewGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneLoad.SwitchScene("LVL1");
        Time.timeScale = 1f;
    }

    public void Continuo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + save.levelId);
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
