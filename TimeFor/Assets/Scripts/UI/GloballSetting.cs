using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class GloballSetting : MonoCache
{
    [Header("Панели")]
    [SerializeField] public PauseScript pauseScript;
    [SerializeField] public InventoryScript inventoryScript;
    [SerializeField] public DeathScript deathScript;
    [SerializeField] public ScrollScript scrollScript;
    [SerializeField] public SettingsScript settingsScript;

    [Header("UI")]
    public GameObject InventoryPanel;
    public GameObject IndecatorPanel;
    public GameObject DeathPanel;
    public GameObject PausePanel;
    public GameObject SettingPanel;

    private Slider SensitivityYSlider;
    private Slider SensitivityXSlider;

    [Header("Объекты")]
    public GameObject character;

    [Header("Управление курсором")]
    [SerializeField] bool isVisible = true;
    [SerializeField] public CinemachineFreeLook freeLook;

    private void Start()
    {
        #region Find Component

        pauseScript = GetComponent<PauseScript>();
        inventoryScript = GetComponent<InventoryScript>();
        deathScript = GetComponent<DeathScript>();
        scrollScript = GetComponent<ScrollScript>();
        settingsScript = GetComponent<SettingsScript>();

        character = GameObject.FindGameObjectWithTag("Player");
        freeLook = GameObject.FindGameObjectWithTag("FreeLook").GetComponent<CinemachineFreeLook>();

        #endregion

        #region Find UI

        InventoryPanel = GameObject.Find("InventoryPanel");
        IndecatorPanel = GameObject.Find("IndecatorsPanel");
        DeathPanel = GameObject.Find("DeathPanel");
        PausePanel = GameObject.Find("PausePanel");
        SettingPanel = GameObject.Find("SettingPanel");

        SensitivityYSlider = GameObject.Find("SensitivityY").GetComponent<Slider>();
        SensitivityXSlider = GameObject.Find("SensitivityX").GetComponent<Slider>();

        #endregion 

        notVisible();

        settingsScript.SetComponent(SettingPanel, PausePanel, freeLook, SensitivityYSlider, SensitivityXSlider);
        scrollScript.SetComponent(freeLook);
        inventoryScript.SetComponent(InventoryPanel, character.GetComponent<CharacterStatus>(), freeLook);
        deathScript.SetComponent(DeathPanel);
        pauseScript.SetComponent(PausePanel);

    }

    public override void OnTick()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseScript.isOpenPanel == false)
        {
            pauseScript.OpenMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && pauseScript.isOpenPanel == false)
        {
            inventoryScript.OpenInventory();
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
