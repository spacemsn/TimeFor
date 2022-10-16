using UnityEngine;
using UnityEngine.SceneManagement;

public class DealthCharacter : MonoBehaviour
{
    public GameObject CharacterPrefab;

    [Header("Меню Респавна")]
    public Transform DealthPanel;
    [SerializeField] public bool isOpenPanel;

    public void SavePosition(Vector3 transform, Quaternion rotate)
    {
        CharacterPrefab.transform.position = transform;
        CharacterPrefab.transform.rotation = rotate;
    }

    public void OpenMenu()
    {
        Health health = GetComponent<Health>();
        if (isOpenPanel == false)
        {
            DealthPanel.gameObject.SetActive(true);
            isOpenPanel = true;
            // Видимость курсора
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
