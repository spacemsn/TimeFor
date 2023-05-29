using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private GloballSetting globallSetting;

    [Header("Меню Смерти")]
    public GameObject DeathPanel;
    [SerializeField] public bool isOpenPanel = true;

    private void Start()
    {
        globallSetting = GetComponent<GloballSetting>();
    }

    public void SetComponent(GameObject DeathPanel)
    {
        this.DeathPanel = DeathPanel;
    }

    public void SavePosition(Vector3 transform, Quaternion rotate)
    {
        character.transform.position = transform;
        character.transform.rotation = rotate;
    }

    public void OpenMenu()
    {
        if (isOpenPanel == false)
        {
            DeathPanel.gameObject.SetActive(true);
            isOpenPanel = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else if(isOpenPanel == true)
        {
            DeathPanel.gameObject.SetActive(false);
            isOpenPanel = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    public void Respawn()
    {
        character = globallSetting.character;
        SceneLoad.SwitchScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void ExitToMenu()
    {
        SceneLoad.SwitchScene("Menu");
        Time.timeScale = 1f;
    }
}
