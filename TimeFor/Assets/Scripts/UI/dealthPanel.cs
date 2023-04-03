using UnityEngine;
using UnityEngine.SceneManagement;

public class dealthPanel : MonoBehaviour
{
    [SerializeField] private GameObject Character;
    [SerializeField] private GloballSetting globallSetting;

    [Header("Меню Смерти")]
    public Transform DealthPanel;
    [SerializeField] public bool isOpenPanel;

    private void Start()
    {
        globallSetting = GetComponent<GloballSetting>();
        Character = GameObject.FindGameObjectWithTag("Player");
    }

    public void SavePosition(Vector3 transform, Quaternion rotate)
    {
        Character.transform.position = transform;
        Character.transform.rotation = rotate;
    }

    public void OpenMenu()
    {
        if (isOpenPanel == false)
        {
            DealthPanel.gameObject.SetActive(true);
            isOpenPanel = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }

    public void Respawn()
    {
        SceneLoad.SwitchScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void ExitToMenu()
    {
        SceneLoad.SwitchScene("Menu");
        Time.timeScale = 1f;
    }
}
