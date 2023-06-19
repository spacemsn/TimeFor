using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class GloballSetting : MonoBehaviour
{
    [Header("EntryPoint")]
    public EntryPoint entryPoint;
    public PlayerEntryPoint playerEntry;
    public UIEntryPoint uIEntry;
    public GloballEntryPoint globallEntry;

    [Header("Панели")]
    [SerializeField] public PauseScript pauseScript;
    [SerializeField] public bookCharacter bookScript;
    [SerializeField] public DeathScript deathScript;
    [SerializeField] public SettingsScript settingsScript;
    [SerializeField] public ElementalScript elementalScript;

    [Header("Объекты")]
    public GameObject player;

    [Header("Управление курсором")]
    [SerializeField] public CinemachineFreeLook freeLook;

    private void Start()
    {
        SpawnContoller.onPlayerSceneLoaded += UpdateStatus;
    }

    public void UpdateStatus()
    {
        if (SpawnContoller.isPlayerSceneLoaded)
        {
            freeLook = playerEntry.currentFreeLook;
            player = playerEntry.currentPlayer;
            notVisible();
        }
        else
        {
            Visible();
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.BackQuote) && !bookScript.isOpenInventory && !bookScript.isOpenMap && !deathScript.isOpenPanel && !settingsScript.isOpenPanel && !elementalScript.isOpenPanel)
            {
                OpenMenu(pauseScript.PausePanel, pauseScript.isOpenPanel);

                if (pauseScript.isOpenPanel)
                {
                    pauseScript.OpenPanel(); notVisible(); ResumeGame();
                }
                else if (!pauseScript.isOpenPanel)
                {
                     pauseScript.OpenPanel(); Visible(); PauseGame(); 
                }
            }
            else if (Input.GetKeyDown(KeyCode.B) && !pauseScript.isOpenPanel)
            {
                OpenMenu(bookScript.inventoryPage, bookScript.isOpenInventory);

                if (bookScript.isOpenInventory)
                {
                     bookScript.OpenInventory(); ResumeGame(); notVisible();
                }
                else if (!bookScript.isOpenInventory)
                {
                    bookScript.OpenInventory(); PauseGame(); Visible();
                }
            }
            else if (Input.GetKeyDown(KeyCode.M) && !pauseScript.isOpenPanel)
            {
                OpenMenu(bookScript.mapPanel, bookScript.isOpenMap);

                if (bookScript.isOpenMap)
                {
                     bookScript.OpenMap(); ResumeGame(); notVisible();
                }
                else if (!bookScript.isOpenMap)
                {
                     bookScript.OpenMap(); PauseGame(); Visible();
                }
            }
            else if (Input.GetKeyDown(KeyCode.K) && !pauseScript.isOpenPanel)
            {
                OpenMenu(elementalScript.ElementalPanel, elementalScript.isOpenPanel);

                if (elementalScript.isOpenPanel)
                {
                    elementalScript.OpenPanel(); ResumeGame(); notVisible();
                }
                else if (!elementalScript.isOpenPanel)
                {
                    elementalScript.OpenPanel(); PauseGame(); Visible();
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                Visible();
            }
            else if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                notVisible();
            }
        }
    }

    public void Visible()
    {
        if (freeLook != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            freeLook.m_XAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisValue = 0;
            freeLook.m_XAxis.m_InputAxisValue = 0;
            entryPoint.player.movement.isManagement = false;
        }
    }

    public void notVisible()
    {
        if (freeLook != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            freeLook.m_XAxis.m_InputAxisName = "Mouse X";
            freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
            entryPoint.player.movement.isManagement = true;
        }
    }

    public void OpenMenu(Transform panel, bool isOpenPanel)
    {
        if (!isOpenPanel)
        {
            panel.gameObject.SetActive(true);
        }
        else if (isOpenPanel)
        {
            panel.gameObject.SetActive(false);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void SlowMode()
    {
        Time.timeScale = 0.5f;
    }
}
