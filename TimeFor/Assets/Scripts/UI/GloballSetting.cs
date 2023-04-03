using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class GloballSetting : MonoCache
{
    [Header("Панели")]
    [SerializeField] public PauseMenu pausePanel;
    [SerializeField] public InventoryPanel inventoryPanel;
    [SerializeField] public dealthPanel dealthPanel;
    [SerializeField] public ScrollCamera scrollCamera;
    [SerializeField] public SettingsMenu settingsMenu;

    [Header("Объекты")]
    public GameObject character;

    [Header("Управление курсором")]
    [SerializeField] bool isVisible = true;
    [SerializeField] private CinemachineFreeLook freeLook;

    private void Start()
    {
        pausePanel = GetComponent<PauseMenu>();
        inventoryPanel = GetComponent<InventoryPanel>();
        dealthPanel = GetComponent<dealthPanel>();
        scrollCamera = GetComponent<ScrollCamera>();

        character = GameObject.FindGameObjectWithTag("Player");
        freeLook = GameObject.FindGameObjectWithTag("FreeLook").GetComponent<CinemachineFreeLook>();

        notVisible();
    }

    public override void OnTick()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pausePanel.isOpenPanel == false)
        {
            pausePanel.OpenMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && pausePanel.isOpenPanel == false)
        {
            inventoryPanel.OpenInventory();
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

    public void Visible()
    {
        if (isVisible == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isVisible = true;
            freeLook.m_XAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisValue = 0;
            freeLook.m_XAxis.m_InputAxisValue = 0;
        }
    }

    public void notVisible()
    {
        if (isVisible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isVisible = false;
            freeLook.m_XAxis.m_InputAxisName = "Mouse X";
            freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
        }
    }

    private void OnEnterButton()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) { Visible(); }
        else if (Input.GetKeyUp(KeyCode.LeftAlt)) { notVisible(); }
    }
}
