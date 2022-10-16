using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneLoad.SwitchScene("LVL1");
        Time.timeScale = 1f;
    }

    public void ExiteGame()
    {
        Application.Quit();
    }
}
