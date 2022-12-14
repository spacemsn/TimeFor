using UnityEngine;
using UnityEngine.SceneManagement;

public class DealthCharacter : MonoBehaviour
{
    public GameObject CharacterPrefab;

    [Header("???? ????????")]
    public Transform DealthPanel;
    [SerializeField] public bool isOpenPanel;

    public void SavePosition(Vector3 transform, Quaternion rotate)
    {
        CharacterPrefab.transform.position = transform;
        CharacterPrefab.transform.rotation = rotate;
    }

    public void OpenMenu()
    {
        if (isOpenPanel == false)
        {
            DealthPanel.gameObject.SetActive(true);
            isOpenPanel = true;
            // ????????? ???????
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
